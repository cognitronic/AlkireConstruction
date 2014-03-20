using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;
using RAM.Services.Messaging.Authentication;

namespace RAM.Services.Interfaces
{
    public interface IHRRMarketingAuthenticationService : ILocalAuthenticationService
    {
        GetUserResponse AuthenticateUser(GetUserRequest request);
    }
}
