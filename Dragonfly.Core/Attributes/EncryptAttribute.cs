using System;

namespace Dragonfly.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptAttribute : Attribute
    {
        public EncryptAttribute() { }
    }
}
