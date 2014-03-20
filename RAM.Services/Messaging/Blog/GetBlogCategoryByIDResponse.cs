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
    public class GetBlogCategoryResponse : Response
    {
        [DataMember]
        public IBlogCategory Category { get; set; }
    }
}
