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
using RAM.Core.Domain.Banner;
using RAM.Core.Domain.Project;

namespace RAM.Controllers.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBannerService _bannerService;
        private readonly IProjectService _projectService;
        private readonly IBlogService _blogService;
        public HomeController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IProjectService projectService,
            IBlogService blogService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IBannerService bannerService,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {
            _bannerService = bannerService;
            _projectService = projectService;
            _blogService = blogService;
        }


        public ActionResult Index()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-home";
            view.Posts = _blogService.GetLatestPosts(2);
            return View(view);

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
            accountView.Banners = _bannerService.GetAll().BannerList;
            return PartialView("_Banners",accountView);

        }

        public ActionResult Portfolio()
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-home";
            view.Projects = _projectService.GetAll().ProjectList.Take(10).ToList();
            return PartialView("_Portfolio", view);

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
