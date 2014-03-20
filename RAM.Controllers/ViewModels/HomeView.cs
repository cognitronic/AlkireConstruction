using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;
using RAM.Core.Domain.Project;
using RAM.Core.Domain.Blog;

namespace RAM.Controllers.ViewModels
{
    public class HomeView
    {
        public HomeView()
        {
            NavView = new NavigationView();
        }
        public NavigationView NavView { get; set; }

        public IList<IBanner> Banners { get; set; }

        public IList<IProject> Projects { get; set; }
        public IProject SelectedProject { get; set; }

        public IList<IBlog> Posts { get; set; }

        public IBlog SelectedPost { get; set; }

        public IList<IBlogCategory> Categories { get; set; }
    }
}
