using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using RAM.Infrastructure.Querying;
using RAM.Infrastructure.UnitOfWork;
using RAM.Repository.NHibernate.SessionStorage;
using NHibernate;

namespace RAM.Repository.NHibernate.Repositories
{
    public abstract class BaseRepository<T, TEntityKey> : IRepository<T> where T : IAggregateRoot, ISystemObject
    {
        private IUnitOfWork _uow;

        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected void Add(T entity)
        {
            SessionFactory.GetCurrentSession().Save(entity);
        }

        protected void Remove(T entity)
        {
            SessionFactory.GetCurrentSession().Delete(entity);
        }

        protected void Save(T entity)
        {
            SessionFactory.GetCurrentSession().SaveOrUpdate(entity);
        }

        public T FindBy(T entity)
        {
            return SessionFactory.GetCurrentSession().Get<T>(entity.ID);
        }

        public T FindBy(TEntityKey id)
        {
            return SessionFactory.GetCurrentSession().Get<T>(id);
        }

        public IList<T> FindAll()
        {
            ICriteria criteriaQuery = SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));
            return (List<T>)criteriaQuery.List<T>();
        }

        public IList<T> FindAll(int index, int count)
        {
            ICriteria criteriaQuery = SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));
            return (List<T>)criteriaQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
        }

        public IList<T> FindBy(Query query)
        {
            ICriteria criteriaQuery = SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));

            AppendCriteria(criteriaQuery);

            query.TranslateIntoNHQuery<T>(criteriaQuery);
            return criteriaQuery.List<T>();
        }

        public IList<T> FindBy(Query query, int index, int count)
        {
            ICriteria criteriaQuery = SessionFactory.GetCurrentSession().CreateCriteria(typeof(T));

            AppendCriteria(criteriaQuery);

            query.TranslateIntoNHQuery<T>(criteriaQuery);
            return criteriaQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
        }

        public virtual void AppendCriteria(ICriteria criteria)
        {

        }

        #region IRepository<T> Members

        int IRepository<T>.Save(T entity)
        {
            this.Save(entity);
            return entity.ID;
        }

        int IRepository<T>.Add(T entity)
        {
            this.Add(entity);
            return entity.ID;
        }

        int IRepository<T>.Remove(T entity)
        {
            int id = entity.ID;
            this.Remove(entity);
            return id;
        }

        #endregion
    }
}
