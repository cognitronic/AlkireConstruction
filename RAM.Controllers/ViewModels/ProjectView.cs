using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAM.Core.Domain.Project;

namespace RAM.Controllers.ViewModels
{
    public class ProjectView
    {
        public ProjectView()
        {
            NavView = new NavigationView();
        }

        public IList<IProject> Projects { get; set; }
        public NavigationView NavView { get; set; }
        public IProject SelectedProject { get; set; }

    }
}