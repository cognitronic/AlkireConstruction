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
    public class BlogCategoryRepository : BaseRepository<BlogCategory, int>, IBlogCategoryRepository, IUnitOfWorkRepository
    {
        public BlogCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((BlogCategory)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((BlogCategory)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((BlogCategory)entity);
        }

        #endregion
    }
}
