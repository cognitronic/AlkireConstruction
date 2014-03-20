using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services;
using RAM.Core.Domain.Blog;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Blog
{
    [Serializable]
    public class GetBlogByTitleResponse : Response
    {
        [DataMember]
        public IBlog BlogPost { get; set; }
    }
}
