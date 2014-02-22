using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Admin.Controllers.ViewModels;
using RAM.Core.Domain.User;
using RAM.Admin.Controllers.ActionArguments;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using System.Web.Mvc;

namespace RAM.Admin.Controllers.Controllers
{
    public class HomeController : BaseUserAccountController
    {
        public HomeController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, formsAuthentication, actionArguments)
        {

        }

        public ActionResult Index()
        {
            UserAccountView view = new UserAccountView();
            view.NavView.SelectedMenuItem = "nav-home";

            view.Message = "It's Working!!!";
            view.CallBackSettings.ReturnUrl = "nav-home";
            return View(view);

        }
    }
}
