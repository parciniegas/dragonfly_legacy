using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System.Web;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Configuration;

namespace Dragonfly.Core.Security
{
    public class AuthenticationFactory : IAuthenticationFactory
    {
        #region Private Fields
        private readonly IApplicationEnvironment _environment;
        private readonly SecurityContext _dbContext;
        private readonly ExceptionManager _exceptionManager;
        #endregion Private Fields

        #region Constructors
        public AuthenticationFactory(IApplicationEnvironment environment)
        {
            _environment = environment;
            _dbContext = new SecurityContext(environment);
            _exceptionManager = new ExceptionManager();
        }

        #endregion Constructors

        #region IAuthenticationFactory Implementations
        public IAuthentication GetAuthenticator(User user)
        {
            Guard.Check(user, "user").IsNotNull();

            var authenticationMethod = 
                _exceptionManager.Process(() => _dbContext.Users
                    .Where(u => u.LoginName == user.LoginName)
                    .Include(p => p.AuthenticationMethod)
                    .First().AuthenticationMethod, "AuthenticationMethod not found from user {0}.".Inject(user.LoginName));

            if ((authenticationMethod.Assembly == "Dragonfly.Core.Security") && (authenticationMethod.Class == "Dragonfly.Core.Security.SqlAuthentication"))
                return new SqlAuthentication(new SecurityContext(_environment));

            var pluginsPath = HttpContext.Current.Server.MapPath("~/bin/plugin/");
            var assemblyName = $"{pluginsPath}{authenticationMethod.Assembly}";
            var className = authenticationMethod.Class;

            var assembly = 
                _exceptionManager.Process(() => Assembly.LoadFrom(assemblyName), "Can't load assembly {0}".Inject(assemblyName));

            var authenticator = 
                _exceptionManager.Process(() => (IAuthentication) assembly.CreateInstance(className), "Couldn't create type instance '{0}'".Inject(className));

            return authenticator;
        }
        #endregion IAuthenticationFactory Implementations
    }
}
