using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Admin.Controllers.ActionArguments;
using RAM.Admin.Controllers.ViewModels;
using System.Web.Mvc;

namespace RAM.Admin.Controllers.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ILocalAuthenticationService authenticationService,
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
            
            return View(accountView);

        }
    }
}
