using System;

namespace Dragonfly.Core
{
    public static class EnumExtensions
    {
        public static string EnumName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }
}
