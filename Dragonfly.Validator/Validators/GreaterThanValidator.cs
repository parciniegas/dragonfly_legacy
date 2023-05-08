﻿using System;
using System.Linq.Expressions;

namespace Dragonfly.Validator.Validators
{
    public sealed class GreaterThanValidator<T, TProperty> : ComparisonValidator<T, TProperty> where TProperty : IComparable
    {
        internal static readonly string Message = ValidatorFactory.Config.DefaultErrorMessageProvider.GetError("NValidator_Validators_GreaterThanValidator_Message");

        public GreaterThanValidator(TProperty value)
            : base(value, Message, LessThanOrEqualValidator<T, TProperty>.Message)
        {
        }

        public GreaterThanValidator(Expression<Func<T, TProperty>> expression)
            : base(expression, Message, LessThanOrEqualValidator<T, TProperty>.Message)
        {
        }

        public override bool IsValid(TProperty value, ValidationContext validationContext)
        {
            if (_value == null)
            {
                return value != null && ((IComparable)value).CompareTo(_value) > 0;
            }
            return ((IComparable)_value).CompareTo(value) < 0;
        }
    }
}
