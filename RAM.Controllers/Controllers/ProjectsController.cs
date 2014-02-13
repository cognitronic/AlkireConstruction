using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAM.Core.Domain;
using RAM.Controllers.ViewModels;

namespace RAM.Controllers.Controllers
{
    public class ProjectsController : Controller
    {
        //
        // GET: /Portfolio/
        public ActionResult Index()
        {
            ProjectView view = new ProjectView();
            view.NavView.SelectedMenuItem = "nav-projects";
            return View(view);
        }

        public ActionResult ByTitle(string category, string title)
        {
            ProjectView view = new ProjectView();
            view.NavView.SelectedMenuItem = "nav-projects";
            return View(view);
        }
	}


}