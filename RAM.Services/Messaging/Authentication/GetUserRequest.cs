using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RAM.Services.Messaging.Authentication
{
    [Serializable]
    public class GetUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
