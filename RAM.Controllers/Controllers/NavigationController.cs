using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Controllers.ActionArguments;
using RAM.Controllers.ViewModels.User;
using RAM.Controllers.ViewModels;
using System.Web.Mvc;

namespace RAM.Controllers.Controllers
{
    public class NavigationController : BaseController
    {
        public NavigationController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {

        }


        public ActionResult Navigate(NavigationView view)
        {
            var home = new HomeView();
            home.NavView.SelectedMenuItem = view.SelectedMenuItem;
            return PartialView("_Navigation", home);

        }
    }
}
