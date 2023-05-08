using Dragonfly.Core.Common;

namespace Dragonfly.Core.Security
{
    public interface IAuthentication
    {
        ICommandResult<User> SignIn(User user);
        ICommandResult SignOut(User user);
    }
}
