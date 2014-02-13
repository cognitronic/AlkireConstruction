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
    public class HomeController : BaseController
    {
        public HomeController(ILocalAuthenticationService authenticationService,
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
            accountView.NavView.SelectedMenuItem = "nav-home";
            return View(accountView);

        }

        public ActionResult TopNavigation()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-home";
            return PartialView("TopNavigation", accountView);

        }

        public ActionResult Banners()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-home";
            return PartialView("Banners",accountView);

        }

        public ActionResult Features()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-home";
            return PartialView("Features", accountView);

        }

        public ActionResult Footer()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-home";
            return PartialView("Footer", accountView);

        }
    }
}
