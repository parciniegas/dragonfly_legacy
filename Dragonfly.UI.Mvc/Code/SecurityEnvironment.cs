using System.Web;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Configuration;

namespace Dragonfly.UI.Mvc.Code
{
    public class SecurityEnvironment : IApplicationEnvironment
    {
       private readonly IConfigurator _configurator;

       public SecurityEnvironment(IConfigurator configurator)
        {
            Guard.Check(configurator, "configurator");
            _configurator = configurator;
        }

        public string GetConnectionString()
        {
            var cs = _configurator.GetKey("securityConnection");
            return cs;
        }

        public string GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}