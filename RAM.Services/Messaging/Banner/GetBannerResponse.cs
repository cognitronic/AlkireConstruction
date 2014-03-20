using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Core.Domain.Banner;

namespace RAM.Services.Messaging.Banner
{
    [Serializable]
    public class GetBannersResponse : Response
    {
        [DataMember]
        public IList<IBanner> BannerList { get; set; } 
    }
}
