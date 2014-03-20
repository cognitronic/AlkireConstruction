using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.User;
using RAM.Infrastructure.Authentication;
using AutoMapper;

namespace RAM.Controllers
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<IUserAccount, User>();
        }
    }
}
