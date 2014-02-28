using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Blog;
using RAM.Infrastructure.Querying;
using RAM.Services.Interfaces;
using RAM.Services.Messaging.Blog;
using RAM.Services.Mapping;
using RAM.Services.Cache;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IBlogTagRepository _blogtagRepository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public BlogService(IBlogRepository repository, IBlogTagRepository blogtagRepository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _blogtagRepository = blogtagRepository;
            _cache = cache;
            _uow = uow;
        }

        #region IBlogService Members

        public GetBlogByTitleResponse GetByTitle(GetBlogByTitleRequest request)
        {
            var response = new GetBlogByTitleResponse();
            Query query = new Query();

            query.Add(new Criterion("Title", request.Title, CriteriaOperator.Equal));

            var post = _repository.FindBy(query);
            if (post != null)
            {
                response.Success = true;
                response.Message = "Blogs Retrieved Successfully!";
                response.BlogPost = post.FirstOrDefault<IBlog>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blogs Retrieved Failed!";
            }

            return response;
        }

        public GetBlogsResponse GetByCategory(GetBlogsByCategoryRequest request)
        {
            var response = new GetBlogsResponse();
            Query query = new Query();
            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                query.Add(new Criterion("Category.Name", request.CategoryName.Replace('-', ' '), CriteriaOperator.Equal));
            }
            else
            {
                query.Add(new Criterion("BlogCategoryID", request.CategoryID, CriteriaOperator.Equal));
            }

            var list = _repository.FindBy(query)
                .Where(o => o.IsActive = true)
                .OrderByDescending(o => o.DatePosted);
            if (list != null)
            {
                response.Success = true;
                response.Message = "Blogs Retrieved Successfully!";
                response.BlogList = list.ToList<IBlog>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blogs Retrieved Failed!";
            }

            return response;
        }

        public GetBlogsResponse GetAll()
        {
            var response = new GetBlogsResponse();
            var list = new List<IBlog>();
            if (_cache.Get<IList<IBlog>>(RAM.Core.ResourceStrings.Cache_BlogPosts) == null)
            {
                list = _repository.FindAll()
                    .Where(o => o.IsActive = true)
                    .OrderByDescending(o => o.DatePosted).ToList<IBlog>();
                _cache.Store(RAM.Core.ResourceStrings.Cache_BlogPosts, list);
            }
            else
            { 
                list = _cache.Get<List<IBlog>>(RAM.Core.ResourceStrings.Cache_BlogPosts);
            }

            if (list != null)
            {
                response.Success = true;
                response.Message = "Blogs Retrieved Successfully!";
                response.BlogList = list.ToList<IBlog>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blogs Retrieved Failed!";
            }

            return response;
        }

        public IList<IBlog> GetLatestPosts(int count)
        {
            var list = new List<IBlog>();
            if (_cache.Get<IList<IBlog>>(RAM.Core.ResourceStrings.Cache_BlogPosts) == null)
            {
                list = _repository.FindAll()
                    .Where(o => o.IsActive = true)
                    .OrderByDescending(o => o.DatePosted).Take(count).ToList<IBlog>();
                _cache.Store(RAM.Core.ResourceStrings.Cache_BlogPosts, list);
            }
            else
            {
                list = _cache.Get<List<IBlog>>(RAM.Core.ResourceStrings.Cache_BlogPosts).Take(count).ToList();
            }
            return list;
        }

        public GetBlogsResponse GetAllForAdmin()
        {
            var response = new GetBlogsResponse();
            var list = new List<IBlog>();
            if (_cache.Get<IList<IBlog>>(RAM.Core.ResourceStrings.Cache_BlogPosts) == null)
            {
                list = _repository.FindAll()
                    .OrderByDescending(o => o.DatePosted).ToList<IBlog>();
                _cache.Store(RAM.Core.ResourceStrings.Cache_BlogPosts, list);
            }
            else
            {
                list = _cache.Get<List<IBlog>>(RAM.Core.ResourceStrings.Cache_BlogPosts);
            }

            if (list != null)
            {
                response.Success = true;
                response.Message = "Blogs Retrieved Successfully!";
                response.BlogList = list.ToList<IBlog>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blogs Retrieved Failed!";
            }

            return response;
        }

        public Blog GetByID(int postID)
        {
            Query query = new Query();
            query.Add(new Criterion("ID", postID, CriteriaOperator.Equal));

            var blog = _repository.FindBy(query).First<Blog>();
            

            return blog;
        }

        public void SavePost(Blog post)
        {
            _repository.Save(post);
            _uow.Commit();
            _cache.Remove(RAM.Core.ResourceStrings.Cache_BlogPosts);
        }

        public void DeletePost(Blog post)
        {
            _repository.Remove(post);
            _uow.Commit();
            _cache.Remove(RAM.Core.ResourceStrings.Cache_BlogPosts);
        }
        #endregion
    

        public void SaveBlogTag(BlogTag blogtag)
        {
            _blogtagRepository.Save(blogtag);
            _uow.Commit();
        }

        public void DeleteBlogTag(BlogTag blogtag)
        {
            _blogtagRepository.Remove(blogtag);
            _uow.Commit();
        }
    }
}
