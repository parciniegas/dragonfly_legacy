using System;
using System.Web;
using System.Web.Mvc;
using Dragonfly.Core.Security;

namespace Dragonfly.Core.Mvc
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeActionAttribute : AuthorizeAttribute
    {
        private readonly string _action;

        public AuthorizeActionAttribute(string action)
        {
            _action = action;
        }

        private static IAuthorization AuthorizationService => DependencyResolver.Current.GetService<IAuthorization>();

        /// <exception cref="ArgumentNullException"><paramref name="httpContext"/> is <see langword="null"/></exception>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            try
            {
                var user = httpContext.User;
                return user.Identity.IsAuthenticated && AuthorizationService.AuthorizeAction(user.Identity.Name, _action);
            }
            catch (NotImplementedException)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            var user = filterContext.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect("~/Account/NotAuthorized");
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
