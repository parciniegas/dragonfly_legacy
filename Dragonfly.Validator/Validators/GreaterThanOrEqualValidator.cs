﻿using System;
using System.Linq.Expressions;

namespace Dragonfly.Validator.Validators
{
    public sealed class GreaterThanOrEqualValidator<T, TProperty> : ComparisonValidator<T, TProperty> where TProperty : IComparable
    {
        internal static readonly string Message = ValidatorFactory.Config.DefaultErrorMessageProvider.GetError("NValidator_Validators_GreaterThanOrEqualValidator_Message");

        public GreaterThanOrEqualValidator(TProperty value)
            : base(value, Message, LessThanValidator<T, TProperty>.Message)
        {
        }

        public GreaterThanOrEqualValidator(Expression<Func<T, TProperty>> expression)
            : base(expression, Message, LessThanValidator<T, TProperty>.Message)
        {
        }

        public override bool IsValid(TProperty value, ValidationContext validationContext)
        {
            if (_value == null)
            {
                return value != null && ((IComparable)value).CompareTo(_value) >= 0;
            }
            return ((IComparable)_value).CompareTo(value) <= 0;
        }
    }
}
