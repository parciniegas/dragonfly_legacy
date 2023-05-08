using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Common;

namespace Dragonfly.Core.Security
{
    public class Authentication : IAuthentication
    {
        #region Private Fields
        private readonly IAuthenticationFactory _authenticationFactory;
        #endregion Private Fields

        public Authentication(IAuthenticationFactory authenticationFactory)
        {
            Guard.Check(authenticationFactory, "authenticationFactory")
                .IsNotNull();

            _authenticationFactory = authenticationFactory;
        }

        public ICommandResult<User> SignIn(User user)
        {
            var check = CheckAuthentication(user);
            if (!check.Success)
                return new CommandResult<User>(check.Success, "0x01", null );
            try
            {
                var authenticator = _authenticationFactory.GetAuthenticator(user);
                var signin = authenticator.SignIn(user);
                return signin.Success
                    ? new CommandResult<User>(true, signin.Result)
                    : new CommandResult<User>(false, "0x01", @"Usuario y/o contraseña inválido", null);
            }
            catch
            {
                return new CommandResult<User>(false, "0x01", @"Usuario y/o contraseña inválido", null);
            }
        }

        public ICommandResult SignOut(User user)
        {
            return new CommandResult(RegisterSignOut(user));
        }

        public ICommandResult ValidateToken(User user, string token)
        {
            throw new System.NotImplementedException();
        }

        #region Private Methods
        private ICommandResult CheckAuthentication(User user)
        {
            if (!VerifyAuthenticationTries(user))
                return new CommandResult(false);

            if (!VerifySessionLimits(user))
                return new CommandResult(false);

            if (!VerifyAuthenticatedUsersQuantity())
                return new CommandResult(false);

            return new CommandResult(true);
        }

        private bool VerifyAuthenticatedUsersQuantity()
        {
            //throw new System.NotImplementedException();
            return true;
        }

        private bool VerifySessionLimits(User user)
        {
            //throw new System.NotImplementedException();
            return true;
        }

        private bool VerifyAuthenticationTries(User user)
        {
            //throw new System.NotImplementedException();
            return true;
        }

        private bool RegisterSignOut(User user)
        {
            throw new System.NotImplementedException();
        }
        #endregion Private Methods
    }
}
