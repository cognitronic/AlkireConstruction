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
            BlogView view = new BlogView();
            view.NavView.SelectedMenuItem = "nav-blog";
            return View(view);

        }

        public ActionResult GetList()
        {
            BlogView view = new BlogView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var response = _blogService.GetAll();
            view.Posts = response.BlogList;
            return PartialView("BlogList", view);
        }

        public ActionResult LatestPosts()
        {
            BlogView view = new BlogView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var response = _blogService.GetAll();
            view.Posts = response.BlogList;
            return PartialView("LatestPosts", view);
        }

        public ActionResult ByTitle(string category, string title)
        {

            BlogView view = new BlogView();
            view.NavView.SelectedMenuItem = "nav-blog";
            GetBlogByTitleRequest request = new GetBlogByTitleRequest();
            request.Title = title.Replace("-", " ");
            var response = _blogService.GetByTitle(request);
            view.SelectedPost = response.BlogPost;
            return View("BlogPost", view);

        }

        public ActionResult ByCategory(string category)
        {
            BlogView view = new BlogView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var request = new GetBlogsByCategoryRequest();
            var catrequest = new GetBlogCategoryByNameRequest();
            catrequest.CategoryName = category.Replace("-", " ");
            request.CategoryID = _categoryService.GetByName(catrequest).Category.ID;
            var response = _blogService.GetByCategory(request);
            view.Posts = response.BlogList;
            return View("BlogsByCategory", view);

        }

        public ActionResult Sidebar(string category)
        {
            BlogView view = new BlogView();
            view.NavView.SelectedMenuItem = "nav-blog";
            var response = _categoryService.GetAll();
            return PartialView("Sidebar", response.Categories);

        }
    }
}
