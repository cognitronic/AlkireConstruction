using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Blog
{
    [Serializable]
    public class GetBlogCategoryByIDRequest
    {
        [DataMember]
        public int CategoryID { get; set; }
    }
}
