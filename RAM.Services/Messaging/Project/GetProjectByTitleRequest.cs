using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Project;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Project
{
    [Serializable]
    public class GetProjectByTitleRequest
    {
        public GetProjectByTitleRequest() { }

        public GetProjectByTitleRequest(string title)
        {
            Title = title;
        }
        [DataMember]
        public string Title { get; set; }
    }
}
