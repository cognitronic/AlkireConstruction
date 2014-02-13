using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.User
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByID(int ID);
    }
}
