using System;

namespace Dragonfly.Core
{
    public static class IntExtensions
    {
        public static void Times(this int @this, Action action)
        {
            for (var i = 1; i <= @this; i++)
            {
                action();
            }
        }
    }
}
