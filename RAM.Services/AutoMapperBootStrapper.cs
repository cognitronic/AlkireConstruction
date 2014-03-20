using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAM.Core.Domain.User;
using RAM.Services.ViewModels;

namespace RAM.Services
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {
            //Users
            Mapper.CreateMap<IUser, UserView>();
        }
    }
}
