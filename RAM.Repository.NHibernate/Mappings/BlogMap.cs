using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Blog;
using RAM.Core.Domain.User;
using FluentNHibernate.Mapping;

namespace RAM.Repository.NHibernate.Mappings
{
    public class BlogMap : ClassMap<Blog>
    {
        public BlogMap()
        {
            Id(x => x.ID);
            Map(x => x.Title);
            Map(x => x.BlogCategoryID);
            Map(x => x.DatePosted);
            Map(x => x.EnteredBy);
            Map(x => x.Post).Length(2046000);
            Map(x => x.PostPreview);
            Map(x => x.SEODescription);
            Map(x => x.IsActive);
            Map(x => x.SEOKeywords);
            Map(x => x.ImagePath);
            HasMany<BlogTag>(x => x.Tags)
                .KeyColumn("BlogID");
            References<BlogCategory>(x => x.Category)
                .Column("BlogCategoryID")
                .NotFound
                .Ignore()
                .Not.Update()
                .Not.Insert();
            References<User>(x => x.EnteredByRef)
                .Column("EnteredBy")
                .NotFound
                .Ignore()
                .Not.Update()
                .Not.Insert();
        }
    }
}
