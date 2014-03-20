using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.User;

namespace RAM.Services.Messaging.UserService
{
    public class GetValidUserResponse
    {
        public IUser SelectedUser { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
