using Dragonfly.Core.Common;
using Dragonfly.Core.Configuration;

namespace Dragonfly.Core.Security
{
    public class OtpService : IOtpService
    {
        private readonly OtpGenerator _otpGenerator;

        public OtpService()
        {
            _otpGenerator = new OtpGenerator();
        }

        public string GetOtp(User user)
        {
            return _otpGenerator.GetOtp(user.OtpKey, user.OtpValidTime);
        }

        public ICommandResult ValidateOtp(User user, string otp)
        {
            return new CommandResult(otp == _otpGenerator.GetOtp(user.OtpKey, user.OtpValidTime));
        }
    }
}
