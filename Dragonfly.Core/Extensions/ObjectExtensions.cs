using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Dragonfly.Core.Attributes;

namespace Dragonfly.Core
{
    public static class ObjectExtensions
    {
        #region Convert Methods
        public static T To<T>(this object @this)
        {
            if ((@this == null) || (@this == DBNull.Value))
                return (T)(object)null;

            var targetType = typeof(T);

            if (@this.GetType() == targetType)
            {
                return (T)@this;
            }

            var converter = TypeDescriptor.GetConverter(@this);
            if (converter.CanConvertTo(targetType))
            {
                return (T)converter.ConvertTo(@this, targetType);
            }

            converter = TypeDescriptor.GetConverter(targetType);
            if (converter.CanConvertFrom(@this.GetType()))
            {
                return (T)converter.ConvertFrom(@this);
            }

            return (T)@this;
        }

        public static object To(this Object @this, Type type)
        {
            if ((@this == null) || (@this == DBNull.Value))
                return null;

            var targetType = type;

            if (@this.GetType() == targetType)
            {
                return @this;
            }

            var converter = TypeDescriptor.GetConverter(@this);
            if (converter.CanConvertTo(targetType))
            {
                return converter.ConvertTo(@this, targetType);
            }

            converter = TypeDescriptor.GetConverter(targetType);

            return converter.CanConvertFrom(@this.GetType()) 
                ? converter.ConvertFrom(@this) 
                : @this;
        }
        #endregion

        #region Reflection
        public static PropertyInfo[] GetProperties<T>(this T @this)
        {
            return @this.GetType().GetProperties();
        }

        public static PropertyInfo[] GetProperties<T>(this T @this, BindingFlags bindingAttr)
        {
            return @this.GetType().GetProperties(bindingAttr);
        }

        public static PropertyInfo GetProperty<T>(this T @this, string name)
        {
            return @this.GetType().GetProperty(name);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
        public static PropertyInfo GetProperty<T>(this T @this, string name, BindingFlags bindingFlags)
        {
            return @this.GetType().GetProperty(name, bindingFlags);
        }

        public static object GetPropertyValue<T>(this T @this, string propertyName)
        {
            var type = @this.GetType();
            var property = type.GetProperty(propertyName);

            return property.GetValue(@this, null);
        }
        #endregion

        #region Null Validations
        public static bool IsNull<T>(this T @this) where T : class
        {
            return @this == null;
        }

        public static void Encrypt<T>(this T @this) where T : class
        {
            var properties = @this.GetProperties().Where(p => p.GetCustomAttributes(typeof(EncryptAttribute)).Any());
            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string)) continue;
                var encrypted = property.GetValue(@this);
                if (encrypted == null) continue;
                encrypted = encrypted.ToString().Encrypt();
                property.SetValue(@this, encrypted);
            }
        }

        public static void Decrypt<T>(this T @this) where T : class
        {
            var properties = @this.GetProperties().Where(p => p.GetCustomAttributes(typeof(EncryptAttribute)).Any());
            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string)) continue;
                var decrypted = property.GetValue(@this);
                if (decrypted == null) continue;
                decrypted = decrypted.ToString().Decrypt();
                property.SetValue(@this, decrypted);
            }
        }
        #endregion
    }
}
