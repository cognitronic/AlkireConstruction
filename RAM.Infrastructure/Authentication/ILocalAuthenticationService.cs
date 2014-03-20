using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Authentication
{
    public interface ILocalAuthenticationService
    {
        IUserAccount Login(string email, string password);
        IUserAccount RegisterUser(string email, string password);
    }
}
