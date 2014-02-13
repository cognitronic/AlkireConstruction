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
    public class ServicesController : BaseController
    {
        public ServicesController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {

        }

        //
        // GET: /Services/
        public ActionResult Index()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-services";
            return View(accountView);
        }

        public ActionResult Industrial()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-services";
            return View(accountView);
        }

        public ActionResult Commercial()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-services";
            return View(accountView);
        }

        public ActionResult Residential()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-services";
            return View(accountView);
        }

        public ActionResult BuildingMaintenance()
        {
            HomeView accountView = new HomeView();
            accountView.NavView.SelectedMenuItem = "nav-services";
            return View(accountView);
        }
	}
}