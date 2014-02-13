using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services;
using RAM.Core.Domain.Project;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Project
{
    [Serializable]
    public class GetProjectByTitleResponse : Response
    {
        [DataMember]
        public IProject ProjectPost { get; set; }
    }
}
