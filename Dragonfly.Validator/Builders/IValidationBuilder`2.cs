using System;
using System.Linq.Expressions;

namespace Dragonfly.Validator.Builders
{
    public interface IValidationBuilder<T, TProperty> : IValidationBuilder<T>,
                                                        IPostInitFluentValidationBuilder<T, TProperty>,
                                                        ICloneable
    {
        Expression<Func<T, TProperty>> Expression { get; set; }
    }
}

