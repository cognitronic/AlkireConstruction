using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;
using RAM.Core.Domain.Project;
using RAM.Core.Domain.Blog;

namespace RAM.Admin.Controllers.ViewModels
{
    public class HomeView
    {
        public HomeView()
        {
            NavView = new NavigationView();
            BlogCategories = new List<IBlogCategory>();
            Tags = new List<Tag>();
        }
        public NavigationView NavView { get; set; }
        public IList<IBanner> Banners { get; set; }
        public IList<IProject> Projects { get; set; }
        public IList<IBlog> Blogs { get; set; }
        public IList<IBlogCategory> BlogCategories { get; set; }
        public IList<Tag> Tags { get; set; }
        public Blog SelectedBlog { get; set; }
        public IList<BlogTag> SelectedBlogTags { get; set; }
    }
}
