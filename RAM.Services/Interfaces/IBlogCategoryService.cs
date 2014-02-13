using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.Messaging.Blog;

namespace RAM.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        GetBlogCategoriesResponse GetAll();
        GetBlogCategoryResponse GetByID(GetBlogCategoryByIDRequest request);
        GetBlogCategoryResponse GetByName(GetBlogCategoryByNameRequest request);
    }
}
