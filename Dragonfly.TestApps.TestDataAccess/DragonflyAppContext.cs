using Dragonfly.Core;
using Dragonfly.Core.Configuration;

namespace Dragonfly.TestApps.TestDataAccess
{
    internal class DragonflyAppContext : IApplicationEnvironment
    {
        public string GetConnectionString()
        {
            return "DragonFly";
        }

        public string GetCurrentUser()
        {
            return "Admin";
        }
    }
}
