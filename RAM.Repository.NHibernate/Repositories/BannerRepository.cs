using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;
using RAM.Infrastructure.Domain;
using RAM.Infrastructure.Querying;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Repository.NHibernate.Repositories
{
    public class BannerRepository : BaseRepository<Banner, int>, IBannerRepository, IUnitOfWorkRepository
    {
        public BannerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((Banner)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((Banner)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((Banner)entity);
        }

        #endregion
    }
}
