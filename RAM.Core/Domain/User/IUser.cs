using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.User
{
    public interface IUser : IBaseUser, IAuditable, ISystemObject
    {
        bool IsActive { get; set; }
        string Password { get; set; }
        string PasswordQuestion { get; set; }
        string PasswordAnswer { get; set; }
        DateTime LastLoginDate { get; set; }
        int AccessLevel { get; set; }
    }
}
