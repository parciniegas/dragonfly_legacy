namespace Dragonfly.Core.Security
{
    public interface IAuthenticationFactory
    {
        IAuthentication GetAuthenticator(User user);
    }
}
