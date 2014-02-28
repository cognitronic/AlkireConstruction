using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Subscriber;
using RAM.Core.Domain.User;
using FluentNHibernate.Mapping;


namespace RAM.Repository.NHibernate.Mappings
{
    public class SubscriberMap : ClassMap<Subscriber>
    {
        public SubscriberMap()
        {
            Id(x => x.ID);
            Map(x => x.DateCreated);
            Map(x => x.Email);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Phone);
        }
    }
}
