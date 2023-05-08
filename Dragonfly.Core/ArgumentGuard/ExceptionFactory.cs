using System;

namespace Dragonfly.Core.ArgumentGuard
{
    internal static class ExceptionFactory
    {
        public static ArgumentException CreateForParamValidation(Param param, string message)
        {
            return new ArgumentException(
                param.ExtraMessageFunc == null
                    ? message
                    : string.Concat(message, Environment.NewLine, param.ExtraMessageFunc()),
                param.Name);
        }

        public static ArgumentNullException CreateForParamNullvalidation(Param param, string message)
        {
            return new ArgumentNullException(
                param.Name,
                param.ExtraMessageFunc == null
                    ? message
                    : string.Concat(message, Environment.NewLine, param.ExtraMessageFunc()));
        }
    }
}
