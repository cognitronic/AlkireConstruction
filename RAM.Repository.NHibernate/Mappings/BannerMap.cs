using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;
using RAM.Core.Domain.User;
using FluentNHibernate.Mapping;

namespace RAM.Repository.NHibernate.Mappings
{
    public class BannerMap : ClassMap<Banner>
    {
        public BannerMap()
        {
            Id(x => x.ID);
            Map(x => x.Title);
            Map(x => x.AltText);
            Map(x => x.ImagePath);
            Map(x => x.SubText1);
            Map(x => x.SubText2);
        }
    }
}
