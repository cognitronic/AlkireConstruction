using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Subscriber;
using RAM.Infrastructure.Domain;
using RAM.Infrastructure.Querying;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Repository.NHibernate.Repositories
{
    public class SubscriberRepository : BaseRepository<Subscriber, int>, ISubscriberRepository, IUnitOfWorkRepository
    {
        public SubscriberRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((Subscriber)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((Subscriber)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((Subscriber)entity);
        }

        #endregion
    }
}
