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
    public class TagRepository: BaseRepository<Tag, int>, ITagRepository, IUnitOfWorkRepository
    {
        public TagRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((Tag)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((Tag)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((Tag)entity);
        }

        #endregion
    }
}
