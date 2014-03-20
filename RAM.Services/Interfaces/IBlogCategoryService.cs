using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.Messaging.Blog;
using RAM.Core.Domain.Blog;

namespace RAM.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        GetBlogCategoriesResponse GetAll();
        BlogCategory GetByID(int id);
        GetBlogCategoryResponse GetByName(GetBlogCategoryByNameRequest request);
        void Save(BlogCategory cat);
        void Delete(BlogCategory cat);
    }
}
