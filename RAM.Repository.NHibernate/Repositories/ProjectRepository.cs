using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Project;
using RAM.Infrastructure.Domain;
using RAM.Infrastructure.Querying;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Repository.NHibernate.Repositories
{
    public class ProjectRepository : BaseRepository<Project, int>, IProjectRepository, IUnitOfWorkRepository
    {
        public ProjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }



        #region IUnitOfWorkRepository Members

        public void PersistCreationOf(IAggregateRoot entity)
        {
            this.Add((Project)entity);
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            this.Save((Project)entity);
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            this.Remove((Project)entity);
        }

        #endregion
    }
}
