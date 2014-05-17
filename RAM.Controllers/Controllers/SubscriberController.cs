using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Controllers.ActionArguments;
using RAM.Controllers.ViewModels;
using RAM.Core.Domain.Subscriber;
using System.Web.Mvc;
using IdeaSeed.Core.Validation;
using IdeaSeed.Core.Utils;

namespace RAM.Controllers.Controllers
{
    public class SubscriberController : BaseController
    {
        private readonly ISubscriberService _subscriberService;
        public SubscriberController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            ISubscriberService subscriberService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {

            _subscriberService = subscriberService;
        }


        public ActionResult Index()
        {
            var view = new HomeView();
            return PartialView("_Subscribe", view);

        }

        public ActionResult SubscribeToNewsletter(string email)
        {
            if (ValidationUtils.IsEmailValid(email)) {
                var s = _subscriberService.GetByEmail(email);
                if (s != null && !string.IsNullOrEmpty(s.Email))
                {
                    return Json(new
                    {
                        Message = "It appears you're already on the list, we appreciate your enthusiasm",
                        Status = "duplication"
                    });
                }
                else
                {
                    s = new Subscriber();
                    s.DateCreated = DateTime.Now;
                    s.Email = email;
                    s.FirstName = "";
                    s.LastName = "";
                    s.Phone = "";
                }
                _subscriberService.Save(s);
                return Json(new
                {
                    Message = "Thanks for your registration!",
                    Status = "success"
                });
            }
            return Json(new
            {
                Message = "Invalid Email address",
                Status = "error"
            });
        }
    }
}
