namespace Dragonfly.Core.Configuration
{
    public interface IApplicationEnvironment
    {
        string GetConnectionString();
        string GetCurrentUser();
    }
}
