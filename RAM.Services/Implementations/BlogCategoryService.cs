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
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public BlogCategoryService(IBlogCategoryRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }

        #region IBlogCategoryService Members

        public GetBlogCategoriesResponse GetAll()
        {
            var response = new GetBlogCategoriesResponse();

            var list = _repository.FindAll().OrderBy(o => o.Name);
            if (list != null)
            {
                response.Success = true;
                response.Message = "Blog Categories Retrieved Successfully!";
                response.Categories = list.ToList<IBlogCategory>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blogs Categories Retrieved Failed!";
            }

            return response;
        }

        public GetBlogCategoryResponse GetByID(GetBlogCategoryByIDRequest request)
        {
            var response = new GetBlogCategoryResponse();
            Query query = new Query();

            query.Add(new Criterion("ID", request.CategoryID, CriteriaOperator.Equal));

            var cat = _repository.FindBy(query);
            if (cat != null)
            {
                response.Success = true;
                response.Message = "Blog Categories Retrieved Successfully!";
                response.Category = (IBlogCategory)cat;
            }
            else
            {
                response.Success = false;
                response.Message = "Blog Categories Retrieved Failed!";
            }

            return response;
        }

        public GetBlogCategoryResponse GetByName(GetBlogCategoryByNameRequest request)
        {
            var response = new GetBlogCategoryResponse();
            Query query = new Query();

            query.Add(new Criterion("Name", request.CategoryName, CriteriaOperator.Equal));

            var cat = _repository.FindBy(query);
            if (cat != null)
            {
                response.Success = true;
                response.Message = "Blog Categories Retrieved Successfully!";
                response.Category = cat.FirstOrDefault<IBlogCategory>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blog Categories Retrieved Failed!";
            }

            return response;
        }

        #endregion
    }
}
