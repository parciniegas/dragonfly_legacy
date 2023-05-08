using System.Diagnostics;

namespace Dragonfly.Core.ArgumentGuard
{
    public static class GuardObjectExtensions
    {
        [DebuggerStepThrough]
        public static Param<T> IsNotNull<T>(this Param<T> param) where T : class
        {
            if (param.Value == null)
                throw ExceptionFactory.CreateForParamNullvalidation(param, ExceptionMessages.EnsureExtensions_IsNotNull.Inject(param.Name));

            return param;
        }
    }
}
