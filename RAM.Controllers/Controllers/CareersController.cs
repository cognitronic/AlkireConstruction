using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Controllers.ActionArguments;
using RAM.Controllers.ViewModels;
using System.Web.Mvc;

namespace RAM.Controllers.Controllers
{
    public class CareersController : BaseController
    {
        public CareersController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {

        }


        public ActionResult Index()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-careers";
            return View(accountView);

        }
    }
}
