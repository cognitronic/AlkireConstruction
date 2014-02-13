using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RAM.Infrastructure.Domain
{
    public class IdentityMap<T>
    {
        Hashtable entities = new Hashtable();

        public T GetByID(Guid systemID)
        {
            if (entities.ContainsKey(systemID))
                return (T)entities[systemID];
            else
                return default(T);
        }

        public void Store(T entity, Guid key)
        {
            if (!entities.ContainsKey(key))
                entities.Add(key, entity);
        }
    }
}
