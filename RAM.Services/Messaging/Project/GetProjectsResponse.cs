using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Core.Domain.Project;

namespace RAM.Services.Messaging.Project
{
    [Serializable]
    public class GetProjectsResponse : Response
    {
        [DataMember]
        public IList<IProject> ProjectList { get; set; } 
    }
}
