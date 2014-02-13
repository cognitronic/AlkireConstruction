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
    public class ProjectImageMap : ClassMap<ProjectImage>
    {
        public ProjectImageMap()
        {
            Id(x => x.ID);
            Map(x => x.AltText);
            Map(x => x.ImagePath);
            Map(x => x.ProjectID);
        }
    }
}
