using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Banner
{
    [Serializable]
    public class GetBannerByTitleRequest
    {
        public GetBannerByTitleRequest() { }

        public GetBannerByTitleRequest(string title)
        {
            Title = title;
        }
        [DataMember]
        public string Title { get; set; }
    }
}
