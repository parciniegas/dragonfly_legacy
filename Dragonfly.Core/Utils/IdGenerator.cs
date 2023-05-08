using System;

namespace Dragonfly.Core
{
    public static class IdGenerator
    {
        public static string Generate()
        {
            var i = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
            return i.ToString().PadLeft(20, '0');
        }
    }
}
