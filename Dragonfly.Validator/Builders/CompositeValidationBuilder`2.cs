﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dragonfly.Validator.Builders
{
    internal class InternalTypeValidator<TItem> : TypeValidator<TItem>
    {
        // Becaue TypeValidator is an abstract class
    }

    /// <summary>
    /// An implementation of validation builder that is used to build validation rules against item inside a collection, a list, an array, etc
    /// This class is used to make the extension method ForEach()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    internal class CompositeValidationBuilder<T, TProperty, TItem> : ValidationBuilder<T, TProperty> where TProperty : IEnumerable<TItem>
    {
        public CompositeValidationBuilder(IValidationBuilder<T, TProperty> previousBuilder) :
            base(previousBuilder.Expression)
        {
            BeforeValidation = previousBuilder.BeforeValidation;
            AfterValidation = previousBuilder.AfterValidation;
            previousBuilder.Next = this;
            Previous = previousBuilder;
            Validator = new InternalTypeValidator<TItem>();
            ContainerName = previousBuilder.ChainName ?? previousBuilder.ContainerName;
        }
        private CompositeValidationBuilder(Expression<Func<T, TProperty>> expression)
            : base(expression)
        {
        }

        protected internal override IEnumerable<ValidationResult> GetResults(ValidationContext validationContext, T containerObject, out string propertyChain)
        {
            propertyChain = ContainerName;
            if (Validator == null || validationContext.ShouldIgnore(propertyChain))
            {
                return Enumerable.Empty<ValidationResult>();
            }
            var enumerable = (TProperty)GetObjectToValidate(containerObject);
            var results = new List<ValidationResult>();
            var index = 0;
            foreach (TItem item in enumerable)
            {
                var newChainName = string.Format("{0}[{1}]", propertyChain, index++);
                Validator.TryUpdateContainerName(newChainName);
                results.AddRange(Validator.GetValidationResult(item, validationContext));
            }
            return results;
        }

        protected override IValidationBuilder<T, TProperty> CreateNewBuilder()
        {
            var newOne = (IValidationBuilder<T, TProperty>)base.Clone();
            newOne.Validator = ValidatorFactory.NullValidator;
            MakeConnectionTo(newOne);
            return newOne;
        }

        public override object  Clone()
        {
            var cloned = new CompositeValidationBuilder<T, TProperty, TItem>(Expression)
            {
                BeforeValidation = BeforeValidation,
                AfterValidation = AfterValidation,
                Validator = Validator,
                ContainerName = ContainerName,
                StopChainOnError = StopChainOnError
            };
            return cloned;
        }
    }
}