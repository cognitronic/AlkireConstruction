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

        public ActionResult SubscribeToNewsletter(Subscriber subscriber)
        { 
            var s = _subscriberService.GetByEmail(subscriber.Email);
            if (s != null && !string.IsNullOrEmpty(s.Email))
            {
                return Json(new
                {
                    Message = "It appears you're already on the list, we appreciate your enthusiasm",
                    Status = "fail"
                });
            }
            else
            {
                s = new Subscriber();
                s.DateCreated = DateTime.Now;
                s.Email = subscriber.Email;
                s.FirstName = "";
                s.LastName = "";
                s.Phone = "";
            }
            _subscriberService.Save(s);
            return Json(new
            {
                Message = "You've been successfully added to the list!",
                Status = "success"
            });
        }
    }
}
