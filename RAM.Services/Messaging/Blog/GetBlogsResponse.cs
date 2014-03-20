using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Core.Domain.Blog;

namespace RAM.Services.Messaging.Blog
{
    [Serializable]
    public class GetBlogsResponse : Response
    {
        [DataMember]
        public IList<IBlog> BlogList { get; set; } 
    }
}
