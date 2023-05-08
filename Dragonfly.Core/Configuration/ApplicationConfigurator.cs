using System.Configuration;

namespace Dragonfly.Core.Configuration
{
    public class ApplicationConfigurator : IConfigurator
    {
        public string GetKey(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                throw new ConfigurationErrorsException("Configuration key {0} is not available".Inject(key));

            return value;
        }
    }
}
