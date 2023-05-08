using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Dragonfly.Core
{
    public static class ReflectionExtensions
    {
        private static bool Is(this Type objectType, Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null, "Type");
            if (objectType == null)
                return false;
            if (type == typeof(object))
                return true;
            if (type == objectType || objectType.GetInterfaces().Any(x => x == type))
                return true;
            return objectType.BaseType != null && objectType.BaseType.Is(type);
        }

        public static bool Is<T>(this object @this)
        {
            Contract.Requires<ArgumentNullException>(@this != null, "Object");
            return @this.Is(typeof(T));
        }

        public static bool Is<T>(this Type objectType)
        {
            Contract.Requires<ArgumentNullException>(objectType != null, "ObjectType");
            return objectType.Is(typeof(T));
        }

        private static bool Is(this object @this, Type type)
        {
            Contract.Requires<ArgumentNullException>(@this != null, "Object");
            Contract.Requires<ArgumentNullException>(type != null, "Type");
            return @this.GetType().Is(type);
        }

        public static bool IsAttributeDefined<T>(this object @this, bool inherit) where T : Attribute
        {
            return @this.GetType().IsDefined(typeof(T), inherit);
        }

        public static bool IsDefined(this MemberInfo element, Type attributeType)
        {
            return Attribute.IsDefined(element, attributeType);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this object @this)
        {
            return @this.GetType().GetProperties();
        }
    }
}
