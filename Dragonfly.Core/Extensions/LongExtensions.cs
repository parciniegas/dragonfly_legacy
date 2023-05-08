using System;

namespace Dragonfly.Core
{
    public static class LongExtensions
    {
        public static void Times(this long @this, Action action)
        {
            for (var i = 0; i < @this; i++)
            {
                action();
            }
        }
    }
}
