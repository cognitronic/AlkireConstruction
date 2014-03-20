using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Subscriber;
using RAM.Infrastructure.Querying;
using RAM.Services.Interfaces;
using RAM.Services.Mapping;
using RAM.Services.Cache;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Services.Implementations
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public SubscriberService(ISubscriberRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }
        public Subscriber GetByID(int subscriberID)
        {
            var query = new Query();
            query.Add(new Criterion("ID", subscriberID, CriteriaOperator.Equal));
            return _repository.FindBy(query).FirstOrDefault<Subscriber>();
        }

        public Subscriber GetByEmail(string email)
        {
            var query = new Query();
            query.Add(new Criterion("Email", email, CriteriaOperator.Equal));
            return _repository.FindBy(query).FirstOrDefault<Subscriber>();
        }

        public IList<Subscriber> GetAll()
        {
            return _repository.FindAll();
        }

        public void Save(Subscriber subscriber)
        {
            _repository.Save(subscriber);
            _uow.Commit();
        }

        public void Delete(Subscriber subscriber)
        {
            _repository.Remove(subscriber);
            _uow.Commit();
        }
    }
}
