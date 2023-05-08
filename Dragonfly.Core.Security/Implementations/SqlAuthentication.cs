using System.Linq;
using Dragonfly.Core.Common;

namespace Dragonfly.Core.Security
{
    internal class SqlAuthentication : IAuthentication
    {
        #region Private Fields
        private readonly SecurityContext _context;
        #endregion Private Fields

        #region Constructors
        public SqlAuthentication(SecurityContext context)
        {
            _context = context;
        }
        #endregion  Constructors

        #region IAuthentication implementation
        public ICommandResult<User> SignIn(User user)
        {
            var localUser = _context.Users.SingleOrDefault(u => u.LoginName == user.LoginName);
            return localUser == null 
                ? new CommandResult<User>(false, null) 
                : new CommandResult<User>(PasswordHash.ValidatePassword(user.Password, localUser.Password), localUser );
        }

        public ICommandResult SignOut(User user)
        {
            throw new System.NotImplementedException();
        }
        #endregion IAuthentication implementation
    }
}
