using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Web.UI;
using RAM.Core.Domain.Blog;
using RAM.Services.Interfaces;
using StructureMap;
using IdeaSeed.Web.UI;

namespace RAM.Web.Controls
{
    public class BlogCategoryDDL : DropDownList
    {
        

        public BlogCategoryDDL()
        {
            IBlogCategoryService _service = ObjectFactory.GetInstance<IBlogCategoryService>();
            
            this.Items.Clear();
            this.EmptyMessage = "-- Select --";
            this.Items.Add(new RadComboBoxItem("", "0"));
            this.Skin = "Metro";
            foreach (var s in _service.GetAll().Categories.OrderBy(o => o.Name))
            {
                this.Items.Add(new RadComboBoxItem(s.Name, s.ID.ToString()));
            }
        }
    }
}
