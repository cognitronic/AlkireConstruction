using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Blog;
using RAM.Infrastructure.Domain;
using RAM.Infrastructure.Querying;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Repository.NHibernate.Repositories
{
    public class BlogRepository : BaseRepository<Blog, int>, IBlogRepository, IUnitOfWorkRepository
    {
        public BlogRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((Blog)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((Blog)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((Blog)entity);
        }

        #endregion
    }
}
