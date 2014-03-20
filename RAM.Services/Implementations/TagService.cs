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
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public TagService(ITagRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }

        public Tag GetByID(int tagID)
        {
            var query = new Query();
            query.Add(new Criterion("ID", tagID, CriteriaOperator.Equal));

            return _repository.FindBy(query).FirstOrDefault<Tag>();
        }

        public Tag GetByName(string name)
        {
            var query = new Query();
            query.Add(new Criterion("Name", name, CriteriaOperator.Equal));

            return _repository.FindBy(query).FirstOrDefault<Tag>();
        }

        public IList<Tag> GetAll()
        {
            return _repository.FindAll();
        }

        public IList<Tag> GetForAutoComplete(string input)
        {
            var query = new Query();
            query.Add(new Criterion("Name", input + "%", CriteriaOperator.Like));
            return _repository.FindBy(query);
        }

        public void SaveTag(Tag tag)
        {
            _repository.Save(tag);
            _uow.Commit();
        }

        public void DeleteTag(Tag tag)
        {
            _repository.Remove(tag);
            _uow.Commit();
        }
    }
}
