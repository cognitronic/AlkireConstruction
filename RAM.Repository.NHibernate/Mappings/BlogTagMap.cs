using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Blog;
using FluentNHibernate.Mapping;

namespace RAM.Repository.NHibernate.Mappings
{
    public class BlogTagMap : ClassMap<BlogTag>
    {
        public BlogTagMap()
        {
            Id(x => x.ID);
            Map(x => x.BlogID);
            Map(x => x.TagID);
            References<Tag>(x => x.TagRef)
               .Column("TagID")
               .NotFound
               .Ignore()
               .Not.Update()
               .Not.Insert();
        }
    }
}
