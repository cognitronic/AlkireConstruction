using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Controllers.ActionArguments;
using RAM.Controllers.ViewModels;
using RAM.Services.Messaging.Blog;
using RAM.Core.Domain.Blog;
using System.Web.Mvc;

namespace RAM.Controllers.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _categoryService;
        public BlogController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments, IBlogService blogService, IBlogCategoryService categoryService)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }


        public ActionResult Index()
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Posts = _blogService.GetLatestPosts(2);
            return View(view);

        }

        public ActionResult GetList()
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var response = _blogService.GetAll();
            view.Categories = _categoryService.GetAll().Categories;
            view.Posts = _blogService.GetLatestPosts(2);
            return PartialView("BlogList", view);
        }

        public ActionResult LatestPosts()
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var response = _blogService.GetAll();
            view.Categories = _categoryService.GetAll().Categories;
            view.Posts = response.BlogList;
            return PartialView("LatestPosts", view);
        }

        public ActionResult ByTitle(string category, string title)
        {

            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            GetBlogByTitleRequest request = new GetBlogByTitleRequest();
            request.Title = title.Replace("-", " ");
            var response = _blogService.GetByTitle(request);
            view.SelectedPost = response.BlogPost;
            view.Posts = _blogService.GetAll().BlogList;
            return View("BlogPost", view);

        }

        public ActionResult ByCategory(string category)
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var request = new GetBlogsByCategoryRequest();
            var catrequest = new GetBlogCategoryByNameRequest();
            catrequest.CategoryName = category.Replace("-", " ");
            request.CategoryID = _categoryService.GetByName(catrequest).Category.ID;
            view.Categories = _categoryService.GetAll().Categories;
            var response = _blogService.GetByCategory(request);
            view.Posts = response.BlogList;
            return View("BlogsByCategory", view);

        }

        public ActionResult Sidebar(string category)
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Categories = _categoryService.GetAll().Categories;
            return PartialView("Sidebar", view);

        }
    }
}
