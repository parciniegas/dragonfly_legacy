using System.Configuration;
using System.Data.Common;

namespace Dragonfly.Core
{
    public static class DbFactory
    {
        public static DbConnection CreateConnection(ConnectionStringSettings settings)
        {
            var locked = new object();
            lock (locked)
            {
                var factory = DbProviderFactories.GetFactory(settings.ProviderName);
                var connection = factory.CreateConnection();
                if (connection == null) 
                    return null;
                connection.ConnectionString = settings.ConnectionString;
                return connection;
            }
        }
    }
}
