using System;
using System.Diagnostics.Contracts;

namespace Dragonfly.Core
{
    public static class GenericObjectExtensions
    {
        public static T Check<T>(this T @object, T defaultValue = default(T))
        {
            return @object.Check(x => x != null, defaultValue);
        }

        public static T Check<T>(this T Object, Predicate<T> predicate, T defaultValue = default(T))
        {
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
            return predicate(Object) ? Object : defaultValue;
        }
    }
}
