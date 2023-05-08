using System.Web.Mvc;
using System.Web.Security;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Configuration;
using Dragonfly.Core.Security;
using Dragonfly.UI.Mvc.Areas.Security.Models;
using Dragonfly.Twitter;

namespace Dragonfly.UI.Mvc.Areas.Security.Controllers
{
    public class AccountController : Controller
    {
        #region Private Fields
        private readonly IAuthentication _authentication;
        private readonly IOtpService _otpService;
        #endregion Private Fields

        #region Constructors
        public AccountController(IAuthentication authentication, IOtpService otpService)
        {
            Guard.Check(authentication, nameof(authentication));
            Guard.Check(otpService, nameof(otpService));

            _authentication = authentication;
            _otpService = otpService;
        }
        #endregion Constructors

        // GET: Security/Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(user);

           var result = _authentication.SignIn(user);

            if (result.Success)
            {
                if (result.Result.RequireOtp)
                {
                    var otp = _otpService.GetOtp(result.Result);

                    var cfg = new ApplicationConfigurator();
                    var twitter = new Twitter.Twitter(cfg);
                    twitter.SendDirectMessage("pad61", $"Dragonfly Code: {otp}");

                    ViewBag.ReturnUrl = returnUrl;
                    var viewUser = new ViewUser { Otp = "", ValidTime = result.Result.OtpValidTime, OtpKey = result.Result.OtpKey };
                    return View("ValidateOtp", viewUser);
                }

                FormsAuthentication.SetAuthCookie(user.LoginName, false);
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/"))
                //{
                //    return Redirect(returnUrl);
                //}
                //return RedirectToAction("Index", "Courses");
                return RedirectToAction("Index", "Courses", new { Area = "" });
            }

            ViewBag.ReturnUrl = returnUrl;
            ModelState.AddModelError("AuthError", result.Message);
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateOtp([Bind]ViewUser viewUser, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(viewUser);

            var user = new User { OtpKey = viewUser.OtpKey, OtpValidTime = viewUser.ValidTime };
            var result = _otpService.ValidateOtp(user, viewUser.Otp);
            
            if (result.Success)
            {
                FormsAuthentication.SetAuthCookie(user.LoginName, false);
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/"))
                //{
                //    return Redirect(returnUrl);
                //}
                return RedirectToAction("Index", "Courses", new { Area = "" });
            }

            ViewBag.ReturnUrl = returnUrl;
            ModelState.AddModelError("AuthError", "OTP Inválido!");
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        [AllowAnonymous]
        public ActionResult SendOtp([Bind]ViewUser viewUser)
        {
            if (!ModelState.IsValid)
                return View("ValidateOtp");

            var user = new User { OtpKey = viewUser.OtpKey, OtpValidTime = viewUser.ValidTime };
            var otp = _otpService.GetOtp(user);

            var cfg = new ApplicationConfigurator();
            var twitter = new Twitter.Twitter(cfg);
            twitter.SendDirectMessage("pad61", $"Dragonfly Code: {otp}");

            return View("ValidateOtp", viewUser);
        }
    }
}