﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dragonfly.Validator.Validators;

namespace Dragonfly.Validator.Builders
{
    public class ValidationBuilder<T, TProperty> : IValidationBuilder<T, TProperty>
    {
        public Action<IValidationBuilder<T>, ValidationContext> BeforeValidation { get; set; }
        public Func<IValidationBuilder<T>, IEnumerable<ValidationResult>, IEnumerable<ValidationResult>> AfterValidation { get; set; }
        public IValidator Validator { get; set; }
        public IValidationBuilder<T> Next { get; set; }
        public IValidationBuilder<T> Previous { get; set; }
        public bool StopChainOnError { get; set; }

        private string _chainName;
        public virtual string ChainName
        {
            get
            {
                if (_chainName == null)
                {
                    _chainName = GetChainFromExpression() ?? ContainerName;
                }
                return _chainName;
            }
            set { _chainName = value; }
        }

        private Expression<Func<T, TProperty>> _expression;
        //NOTE: It's not neccessary to be virtual
        public Expression<Func<T, TProperty>> Expression 
        {
            get
            {
                return _expression;
            }
            set { 
                _expression = value;
                _chainName = null;
            }
        }
        
        private string _containerName;
        //NOTE: It's not neccessary to be virtual
        public string ContainerName { 
            get
            {
                return _containerName;
            } 
            protected set 
            { 
                _containerName = value;
                _chainName = null;
            }
        }
        
        public ValidationBuilder(Expression<Func<T, TProperty>> expression)
        {
            Expression = expression;
        }

        public void UpdateContainerName(string containerName)
        {
            ContainerName = containerName;
        }

        protected virtual object GetObjectToValidate(T value)
        {
            try
            {
                return Expression.Compile()(value);
            }
            catch
            {
                return null;
            }
        }

        private string GetChainFromExpression()
        {
            var ex = Expression;
            if (ex != null && ex.NodeType == ExpressionType.Lambda)
            {
                if (ex.Body.NodeType == ExpressionType.MemberAccess || ex.Body.NodeType == ExpressionType.ArrayIndex)
                {
                    var parameter = ex.Parameters[0].ToString();
                    var result = ex.Body.ToString().Remove(0, parameter.Length + 1);
                    return string.Format("{0}.{1}", ContainerName ?? typeof(T).Name, result);
                }

                // NOTE: IF the container is null, the chain will be resolved in GetResults()
                //if (ex.Body.NodeType == ExpressionType.Parameter)
                //{
                //    var parameter = ex.Parameters[0].ToString();
                //    var result = ex.Body.ToString();
                //    if (parameter == result)
                //    {
                //        return typeof (T).Name;
                //    }
                //}
            }
            return null;
        }
        
        public IValidationBuilder<T, TProperty> ToBuilder()
        {
            return this;
        }

        public IValidationBuilder<T, TProperty> SetValidator<TValidator>() where TValidator : class, IValidator<TProperty>
        {
            Validator = new LazyValidator<TValidator, TProperty>();
            return CreateNewBuilder();
        }

        public virtual IValidationBuilder<T, TProperty> SetValidator(IValidator<TProperty> validator)
        {
            Validator = validator;
            return CreateNewBuilder();
        }

        public virtual IValidationBuilder<T> SetValidator(IValidator validator)
        {
            Validator = validator;
            return CreateNewBuilder();
        }

        public IEnumerable<ValidationResult> Validate(T containerObject, ValidationContext validationContext)
        {
            validationContext = validationContext ?? new ValidationContext();
            validationContext.ContainerInstance = containerObject;

            return InternalValidate(containerObject, validationContext);
        }

        protected internal IEnumerable<ValidationResult> InternalValidate(T containerObject, ValidationContext validationContext)
        {
            if (BeforeValidation != null)
            {
                BeforeValidation(this, validationContext);
            }

            string propertyChain;
            IEnumerable<ValidationResult> results = GetResults(validationContext, containerObject, out propertyChain);

            if (AfterValidation != null)
            {
                results = AfterValidation(this, results);
            }
            foreach (var modelValidationResult in results)
            {
                if (validationContext.ShouldIgnore(modelValidationResult.MemberName))
                {
                    continue; // foreach
                }
                yield return FormatValidationResult(modelValidationResult, propertyChain);
            }
        }

        internal protected virtual IEnumerable<ValidationResult> GetResults(ValidationContext validationContext, T containerObject, out string propertyChain)
        {
            propertyChain = ChainName ?? ContainerName ?? typeof(T).Name;
            if (Validator == null || validationContext.ShouldIgnore(propertyChain))
            {
                return Enumerable.Empty<ValidationResult>();
            }
            // NOTE: THe validators can be cached so we need to update their's container name before validation
            Validator.TryUpdateContainerName(propertyChain);
            var value = GetObjectToValidate(containerObject);
            return Validator.GetValidationResult(value, validationContext);
        }

        protected virtual ValidationResult FormatValidationResult(ValidationResult result, string propertyChain)
        {
            result.MemberName = result.MemberName ?? propertyChain;
            if (string.IsNullOrEmpty(result.PropertyName) && result.MemberName != null)
            {
                if (result.MemberName.Contains("."))
                {
                    result.PropertyName = result.MemberName.Substring(result.MemberName.LastIndexOf(".") + 1,
                                                                      result.MemberName.Length -
                                                                      result.MemberName.LastIndexOf(".") - 1);
                }
                else
                {
                    result.PropertyName = result.MemberName;
                }
            }
            result.Message = result.Message.Replace("@PropertyName", result.PropertyName);
            return result;
        }

        protected virtual IValidationBuilder<T, TProperty> CreateNewBuilder()
        {
            var newOne = (IValidationBuilder<T, TProperty>) Clone();
            newOne.Validator = ValidatorFactory.NullValidator;
            MakeConnectionTo(newOne);
            return newOne;
        }

        protected void MakeConnectionTo(IValidationBuilder<T, TProperty> newBuilder)
        {
            newBuilder.Previous = this;
            newBuilder.Next = null;
            Next = newBuilder;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current builder without the connection to other builders
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public virtual object Clone()
        {
            var b = ValidationBuilderHelpers.CreateGenericBuilder(Expression, ValidatorFactory.Config.DefaultValidationBuilderType);
            b.UpdateContainerName(ContainerName);
            b.BeforeValidation = BeforeValidation;
            b.StopChainOnError = StopChainOnError;
            b.Validator = Validator;
            b.UpdateContainerName(ContainerName);
            return b;
        }
    }
}