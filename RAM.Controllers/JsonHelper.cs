using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RAM.Controllers
{
    public class JsonHelper
    {
        public static string SerializeCollection(object list)
        {
            if (list != null)
                return JsonConvert.SerializeObject(list, new JsonSerializerSettings() { ContractResolver = new RAM.Repository.NHibernate.NHibernateContractResolver() });
            return "";
        }
    }
}
