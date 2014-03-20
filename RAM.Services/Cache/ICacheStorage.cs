using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Services.Cache
{
    public interface ICacheStorage
    {
        void Remove(string key);
        void Store(string key, object data);
        T Get<T>(string key);
    }
}
