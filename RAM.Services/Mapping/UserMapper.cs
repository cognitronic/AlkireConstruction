using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.ViewModels;
using RAM.Core.Domain.User;
using AutoMapper;


namespace RAM.Services.Mapping
{
    public static class UserMapper
    {
        public static UserView ConvertToUserView(this IUser user)
        {
            return Mapper.Map<IUser, UserView>(user);
        }
    }
}
