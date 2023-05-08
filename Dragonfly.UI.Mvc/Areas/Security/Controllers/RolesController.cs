using System.Linq;
using System.Web.Mvc;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Security.Services;

namespace Dragonfly.UI.Mvc.Areas.Security.Controllers
{
    public class RolesController : Controller
    {
        private readonly ISecurityService _securityService;

        public RolesController(ISecurityService securityService)
        {
            Guard.Check(securityService, "securityService").IsNotNull();

            _securityService = securityService;
        }

        // GET: Security/Roles
        public ActionResult Index()
        {
            ViewBag.BreadCrum = "Security / Roles";
            return View();
        }

        public ActionResult Edit(int id)
        {
            var rol = _securityService.GetRol(id);
            throw new System.NotImplementedException();
        }

        public ActionResult Grid(string param)
        {
            var roles = _securityService.GetRoles().ToList().OrderBy(r => r.Name);
            return PartialView("_Grid", roles);
        }
    }
}