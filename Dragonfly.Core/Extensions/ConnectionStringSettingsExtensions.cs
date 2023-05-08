using System.Configuration;
using System.Data.Common;

namespace Dragonfly.Core
{
    public static class ConnectionStringSettingsExtensions
    {
        public static DbConnection Connection(this ConnectionStringSettings @this)
        {
            return DbFactory.CreateConnection(@this);
        }
    }
}
