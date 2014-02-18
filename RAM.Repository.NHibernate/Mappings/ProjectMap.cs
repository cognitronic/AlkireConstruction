using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Project;
using RAM.Core.Domain.User;
using FluentNHibernate.Mapping;

namespace RAM.Repository.NHibernate.Mappings
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.ID);
            Map(x => x.Title);
            Map(x => x.Category);
            Map(x => x.DateCreated);
            Map(x => x.DefaultImagePath);
            Map(x => x.Description);
            Map(x => x.EnteredBy);
            Map(x => x.ProjectDate);
            HasMany<ProjectImage>(x => x.Images)
                .KeyColumn("ProjectID");
        }
    }
}
