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
using RAM.Core.Domain.Banner;
using System.Configuration;


namespace RAM.Admin.Controllers.Controllers
{
    public class BannersController : BaseUserAccountController
    {
        private readonly IBannerService _bannerService;
        public BannersController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IBannerService bannerService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, formsAuthentication, actionArguments)
        {
            _bannerService = bannerService;
        }

        public ActionResult Index()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-banners";
            view.Banners = _bannerService.GetAll().BannerList;
            return View(view);

        }

        public ActionResult BannerList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-banners";
            view.Banners = _bannerService.GetAll().BannerList;
            return PartialView("_BannerList", view);
        }

        public ActionResult SaveBanner()
        {
            var banner = new Banner();
            if (Request.Form.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.Form["bannerID"]))
                {
                    banner = _bannerService.GetByID(Convert.ToInt16(Request.Form["bannerID"]));
                }
                banner.Title = Request.Form["title"];
                banner.SubText1 = Request.Form["subText"];
            }
            foreach (string fileName in Request.Files)
            {
                try
                {
                    var file = Request.Files[fileName];
                    banner.ImagePath = ConfigurationSettings.AppSettings["BannerImageURL"] + file.FileName;
                    file.SaveAs(ConfigurationSettings.AppSettings["BannerImageDir"] + file.FileName);
                }
                catch (Exception fileException)
                {
                    return Json(new 
                        { 
                            Message = "File failed to save with following error: " + fileException.Message,
                            Status = "failed"
                        });
                }
            }
            try
            {
                banner.AltText = banner.Title;
                _bannerService.SaveBanner(banner);
            }
            catch(Exception exc)
            {
                return Json(new 
                        {
                            Message = "Banner failed to save with following error: " + exc.Message,
                            Status = "failed"
                        });
            }

            return Json(new 
                        {
                            Message = "Banner saved!",
                            Status = "success",
                            ReturnUrl = "/Banners"
                        });

        }

        public ActionResult DeleteBanner(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var b = _bannerService.GetByID(Convert.ToInt16(id));
                _bannerService.DeleteBanner(b);
            }
            return Json(new
            {
                Message = "Banner saved!",
                Status = "success",
                ReturnUrl = "/Banners"
            });
        }
    }
}
