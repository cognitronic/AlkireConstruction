using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.Messaging.Blog;
using RAM.Core.Domain.Blog;

namespace RAM.Services.Interfaces
{
    public interface IBlogService
    {
        GetBlogByTitleResponse GetByTitle(GetBlogByTitleRequest request);
        GetBlogsResponse GetByCategory(GetBlogsByCategoryRequest request);
        Blog GetByID(int postID);
        GetBlogsResponse GetAll();
        GetBlogsResponse GetAllForAdmin();
        void SavePost(Blog post);
        void DeletePost(Blog post);

        IList<IBlog> GetLatestPosts(int count);
        void SaveBlogTag(BlogTag blogtag);
        void DeleteBlogTag(BlogTag blogtag);

    }
}
