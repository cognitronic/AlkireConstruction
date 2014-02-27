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
    public class BlogTagRepository : BaseRepository<BlogTag, int>, IBlogTagRepository, IUnitOfWorkRepository
    {
        public BlogTagRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((BlogTag)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((BlogTag)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((BlogTag)entity);
        }

        #endregion
    }
}
