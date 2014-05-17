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
using System.Web;
using System.IO;
using System.Configuration;
using RAM.Core;

namespace RAM.Admin.Controllers.Controllers
{
    public class UsersController : BaseUserAccountController
    {
        public UsersController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, formsAuthentication, actionArguments)
        {

        }

        public ActionResult Index()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-users";
            view.Users = _userService.FindAll();
            return View(view);

        }

        public ActionResult UserList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-users";
            view.Users = _userService.FindAll();

            return PartialView("_UserList", view);
        }


        public ActionResult SaveUser(User user)
        {
            var u = new User();


            if (user == null || user.ID < 1)
            {
                u.EnteredBy = SecurityContextManager.Current.CurrentUser.ID;
                u.DateCreated = DateTime.Now;
                u.AccessLevel = 60;
                u.LastUpdated = DateTime.Now;
                u.Email = user.Email;
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Password = user.Password;
                u.ChangedBy = SecurityContextManager.Current.CurrentUser.ID;
                _userService.CreateNewUser(u);
            }
            else
            {
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Email = user.Email;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    u.Password = user.Password;
                }
                u.LastUpdated = DateTime.Now;
                u.ChangedBy = SecurityContextManager.Current.CurrentUser.ID;
                _userService.UpdateUser(u);
            }

            return Json(new
            {
                Message = "user saved!",
                Status = "success",
                UserID = user.ID
            });
        }

        public ActionResult DeleteUser(string id)
        {
            _userService.DeleteUser(_userService.FindByID(Convert.ToInt16(id)));
            return Json(new
            {
                Message = "user deleted!",
                Status = "success"
            });
        }
    }
}
