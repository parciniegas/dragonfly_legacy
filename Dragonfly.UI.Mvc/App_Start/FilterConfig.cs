using System.Web;
using System.Web.Mvc;
using Dragonfly.Core.Mvc;

namespace Dragonfly.UI.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TraceAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
