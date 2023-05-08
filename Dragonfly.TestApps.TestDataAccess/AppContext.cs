using Dragonfly.Core;
using Dragonfly.Core.Configuration;

namespace Dragonfly.TestApps.TestDataAccess
{
    public class AppContext : IApplicationEnvironment
    {
        public string GetConnectionString()
        {
            return "test";
        }

        public string GetCurrentUser()
        {
            return "Admin";
        }
    }
}