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
using IdeaSeed.Core.Mail;
using System.Configuration;



namespace RAM.Controllers.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IBlogService _blogService;
        public ContactController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IBlogService blogService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {
            _blogService = blogService;
        }


        public ActionResult Index()
        {
            var view = new HomeView();
            view.Posts = _blogService.GetLatestPosts(2);
            view.NavView.SelectedMenuItem = "nav-contact";
            return View(view);

        }

        public ActionResult SendMessage()
        {
            string body = "<b>Name: </b>" + Request.Form["field_name"] + "<br />" +
                "<b>Email: </b>" + Request.Form["field_email"] + "<br />" +
                "<b>Phone: </b>" + Request.Form["field_phone"] + "<br />" +
                "<b>Message: </b>" + Request.Form["field_message"] + "<br />";
            try
            {
                EmailUtils.SendEmail(ConfigurationSettings.AppSettings["ContactMessageToAddress"],
                    ConfigurationSettings.AppSettings["ContactMessageFromAddress"],
                    "",
                    ConfigurationSettings.AppSettings["ContactMessageBccAddress"],
                    Request.Form["field_subject"],
                    body,
                    false,
                    ""
                    );
            }
            catch (Exception exc)
            {
                return Json(new
                {
                    Message = "Message did not send.  Please call " + ConfigurationSettings.AppSettings["PhoneNumber"],
                    Status = "fail"
                });
            }
            return Json(new
            {
                Message = "Your message has been received and someone will be in contact shortly, thanks!!",
                Status = "success"
            });
        }
    }
}
