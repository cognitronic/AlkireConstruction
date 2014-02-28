using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using RAM.Controllers.ActionArguments;
using RAM.Controllers.ViewModels;
using RAM.Services.Messaging.Project;
using RAM.Core.Domain.Project;
using System.Web.Mvc;

namespace RAM.Controllers.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IProjectService _projectService;
        public ProjectsController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IBlogService blogService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments, IProjectService projectService)
            : base(authenticationService, userService, externalAuthenticationService, actionArguments)
        {
            _blogService = blogService;
            _projectService = projectService;
        }

        //
        // GET: /Portfolio/
        public ActionResult Index()
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-projects";
            var response = _projectService.GetAll();
            view.Posts = _blogService.GetLatestPosts(2);
            view.Projects = response.ProjectList;
            return View(view);
        }

        public ActionResult ByTitle(string category, string title)
        {
            var view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-projects";
            view.Posts = _blogService.GetLatestPosts(2);
            view.Projects = _projectService.GetByCategory(new GetProjectsByCategoryRequest((int)Enum.Parse(typeof(ProjectType), category), "")).ProjectList;
            view.SelectedProject = _projectService.GetByTitle(new GetProjectByTitleRequest(title.Replace("-", " "))).ProjectPost;
            return View(view);
        }
	}


}