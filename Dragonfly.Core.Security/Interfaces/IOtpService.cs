using Dragonfly.Core.Common;

namespace Dragonfly.Core.Security
{
    public interface IOtpService
    {
        string GetOtp(User user);
        ICommandResult ValidateOtp(User user, string otp);
    }
}
