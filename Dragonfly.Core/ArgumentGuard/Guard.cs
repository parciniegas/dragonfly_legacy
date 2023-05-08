using System.Diagnostics;

namespace Dragonfly.Core.ArgumentGuard
{
    [DebuggerStepThrough]
    public static class Guard
    {
        public static Param<T> Check<T>(T value, string name = Param.DefaultName)
        {
            return new Param<T>(name, value);
        }
    }
}
