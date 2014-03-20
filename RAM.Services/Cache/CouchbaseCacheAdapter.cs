using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Enyim.Caching.Memcached;

namespace RAM.Services.Cache
{
    public class CouchbaseCacheAdapter : ICacheStorage
    {
        private readonly static CouchbaseClient _instance;

        static CouchbaseCacheAdapter()
        {
            _instance = new CouchbaseClient();
        }

        public static CouchbaseClient Instance { get { return _instance; } }

        public void Remove(string key)
        {
            Instance.Remove(key);
        }

        public void Store(string key, object data)
        {
            Instance.Store(StoreMode.Add, key, data);
        }

        public T Get<T>(string key)
        {
            T itemStored = (T)Instance.Get<T>(key);
            if (itemStored == null)
                itemStored = default(T);
            return itemStored;
        }
    }
}
