using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;

namespace RAM.Infrastructure.Authentication
{
    public interface ISecurityContext
    {
        bool IsAuthenticated { get; set; }
        IBaseUser CurrentUser { get; set; }
        string BaseURL { get; set; }
    }
}
