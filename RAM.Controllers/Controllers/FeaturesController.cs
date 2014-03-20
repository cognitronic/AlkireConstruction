using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Controllers.ActionArguments;
using RAM.Controllers.ViewModels.User;
using System.Web.Mvc;

namespace RAM.Controllers.Controllers
{
    public class FeaturesController : BaseController
    {
        public FeaturesController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {

        }

        [ActionName("Team-Communication")]
        public ActionResult TeamCommunication()
        {
            return View("TeamCommunication");
        }
    }
}
