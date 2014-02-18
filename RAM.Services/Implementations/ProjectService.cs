using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Project;
using RAM.Infrastructure.Querying;
using RAM.Services.Interfaces;
using RAM.Services.Messaging.Project;
using RAM.Services.Mapping;
using RAM.Services.Cache;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public ProjectService(IProjectRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }
        public GetProjectByTitleResponse GetByTitle(GetProjectByTitleRequest request)
        {
            var response = new GetProjectByTitleResponse();
            Query query = new Query();

            query.Add(new Criterion("Title", request.Title, CriteriaOperator.Equal));

            var post = _repository.FindBy(query);
            if (post != null)
            {
                response.Success = true;
                response.Message = "Project Retrieved Successfully!";
                response.ProjectPost = post.FirstOrDefault<IProject>();
            }
            else
            {
                response.Success = false;
                response.Message = "Project Retrieved Failed!";
            }

            return response;
        }

        public GetProjectsResponse GetByCategory(GetProjectsByCategoryRequest request)
        {
            var response = new GetProjectsResponse();
            Query query = new Query();
            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                query.Add(new Criterion("Category.Name", request.CategoryName.Replace('-', ' '), CriteriaOperator.Equal));
            }
            else
            {
                query.Add(new Criterion("Category", request.CategoryID, CriteriaOperator.Equal));
            }

            var list = _repository.FindBy(query)
                .OrderByDescending(o => o.DateCreated);
            if (list != null)
            {
                response.Success = true;
                response.Message = "Projects Retrieved Successfully!";
                response.ProjectList = list.ToList<IProject>();
            }
            else
            {
                response.Success = false;
                response.Message = "Projects Retrieved Failed!";
            }

            return response;
        }

        public Core.Domain.Project.Project GetByID(int postID)
        {
            throw new NotImplementedException();
        }

        public GetProjectsResponse GetAll()
        {
            var response = new GetProjectsResponse();
            var list = new List<IProject>();
            if (_cache.Get<IList<IProject>>(RAM.Core.ResourceStrings.Cache_Projects) == null)
            {
                list = _repository.FindAll()
                    .OrderByDescending(o => o.DateCreated).ToList<IProject>();
                _cache.Store(RAM.Core.ResourceStrings.Cache_Projects, list);
            }
            else
            {
                list = _cache.Get<List<IProject>>(RAM.Core.ResourceStrings.Cache_Projects);
            }

            if (list != null)
            {
                response.Success = true;
                response.Message = "Projects Retrieved Successfully!";
                response.ProjectList = list.ToList<IProject>();
            }
            else
            {
                response.Success = false;
                response.Message = "Blogs Retrieved Failed!";
            }

            return response;
        }

        public GetProjectsResponse GetAllForAdmin()
        {
            throw new NotImplementedException();
        }

        public void SavePost(Core.Domain.Project.Project post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Core.Domain.Project.Project post)
        {
            throw new NotImplementedException();
        }
    }
}
