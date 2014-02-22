using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.User;
using FluentNHibernate.Mapping;

namespace RAM.Repository.NHibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.ID);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.AccessLevel);
            Map(x => x.EnteredBy);
            Map(x => x.ChangedBy);
            Map(x => x.DateCreated);
            Map(x => x.LastUpdated);
        }
    }
}
