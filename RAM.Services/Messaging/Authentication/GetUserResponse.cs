using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Core.Domain.User;

namespace RAM.Services.Messaging.Authentication
{
    [Serializable]
    public class GetUserResponse : Response
    {
        public IUser User { get; set; }
    }
}
