using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Blog;

namespace RAM.Controllers.ViewModels
{
    public class BlogView
    {
        public BlogView()
        {
            NavView = new NavigationView();
        }
        public IList<IBlog> Posts { get; set; }
        public NavigationView NavView { get; set; }
        public IBlog SelectedPost { get; set; }

        public IList<IBlogCategory> Categories { get; set; }
    }
}
