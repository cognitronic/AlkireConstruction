using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Querying;

namespace RAM.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T> where T : IAggregateRoot
    {
        T FindBy(T entity);
        IList<T> FindAll();
        IList<T> FindAll(int index, int count);
        IList<T> FindBy(Query query);
        IList<T> FindBy(Query query, int index, int count);
    }
}
