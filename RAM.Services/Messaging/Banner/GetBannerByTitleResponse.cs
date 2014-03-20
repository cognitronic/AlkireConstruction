using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services;
using RAM.Core.Domain.Banner;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Banner
{
    [Serializable]
    public class GetBannerByTitleResponse : Response
    {
        [DataMember]
        public IBanner BannerPost { get; set; }
    }
}
