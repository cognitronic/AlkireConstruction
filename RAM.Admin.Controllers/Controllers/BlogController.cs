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
using RAM.Core.Domain.Blog;
using System.Configuration;

namespace RAM.Admin.Controllers.Controllers
{
    public class BlogController : BaseUserAccountController
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _categoryService;
        private readonly ITagService _tagService;
        public BlogController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IBlogService blogService,
            ITagService tagService,
            IBlogCategoryService categoryService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, formsAuthentication, actionArguments)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;

        }

        public ActionResult Index()
        {
            HomeView view = new HomeView();
            view.BlogCategories = _categoryService.GetAll().Categories;
            view.Tags = _tagService.GetAll();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Blogs = _blogService.GetAll().BlogList;
            return View(view);

        }

        public ActionResult BlogList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Blogs = _blogService.GetAll().BlogList;

            return PartialView("_BlogList", view);

        }
    }
}
