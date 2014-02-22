using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;

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
    }
}
