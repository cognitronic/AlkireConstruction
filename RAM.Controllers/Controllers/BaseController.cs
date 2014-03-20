using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using RAM.Controllers.ActionArguments;
using RAM.Infrastructure.Authentication;
using RAM.Core.Domain.User;
using RAM.Services.Interfaces;
using RAM.Core;

namespace RAM.Controllers.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILocalAuthenticationService _authenticationService;
        protected readonly IUserService _userService;
        protected readonly IExternalAuthenticationService _externalAuthenticationService;
        protected readonly IActionArguments _actionArguments;

        public BaseController(
            ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IActionArguments actionArguments)
        {
            _actionArguments = actionArguments;
            _authenticationService = authenticationService;
            _externalAuthenticationService = externalAuthenticationService;
            _userService = userService;


        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //if (SecurityContextManager.Current == null || SecurityContextManager.Current.CurrentUser == null)
            //{
            //    var url = new UrlHelper(filterContext.RequestContext);
            //    var logonUrl = url.Action("Index", "Login", new { reason = "NotAuthorized" });
            //    filterContext.Result = new RedirectResult(logonUrl);
            //}

            //var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
            //                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
            //                    typeof(AllowAnonymousAttribute), true);
            //if (!skipAuthorization)
            //{
            //    base.OnAuthorization(filterContext);
            //    if (SecurityContextManager.Current != null &&
            //        SecurityContextManager.Current.CurrentUser != null &&
            //        !SecurityContextManager.Current.IsAuthenticated)
            //    {
            //        var url = new UrlHelper(filterContext.RequestContext);
            //        var logonUrl = url.Action("Index", "Login", new { reason = "NotAuthorized" });
            //        filterContext.Result = new RedirectResult(logonUrl);

            //    }
            //}
        }

        public ActionResult RedirectBasedOn(string returnURL)
        {
            if (returnURL == ActionArgumentKey.GoToProfile.ToString())
                return RedirectToAction("Profile", "Profile");
            else if (returnURL == ActionArgumentKey.Login.ToString())
                return RedirectToAction("Index", "Login");
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionArgumentKey GetReturnActionFrom(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().Contains("profile"))
                return ActionArgumentKey.GoToProfile;
            else
                return ActionArgumentKey.Login;
        }
    }
}
