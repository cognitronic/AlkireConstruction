using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Domain
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : IAggregateRoot
    {
        int Save(T entity);
        int Add(T entity);
        int Remove(T entity);
    }
}
