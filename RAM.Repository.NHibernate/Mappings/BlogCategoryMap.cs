using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Blog;
using FluentNHibernate.Mapping;

namespace RAM.Repository.NHibernate.Mappings
{
    public class BlogCategoryMap : ClassMap<BlogCategory>
    {
        public BlogCategoryMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            //HasMany<Blog>(x => x.Posts);
        }
    }
}
