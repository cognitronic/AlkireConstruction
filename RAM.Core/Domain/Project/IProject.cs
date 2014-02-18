using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using RAM.Core.Domain.User;

namespace RAM.Core.Domain.Project
{
    public interface IProject : ISystemObject
    {
        int ID { get; set; }
        string Title { get; set; }
        int Category { get; set; }
        IList<ProjectImage> Images { get; set; }
        DateTime ProjectDate { get; set; }
        string Description { get; set; }
        string DefaultImagePath { get; set; }
        DateTime DateCreated { get; set; }
        int EnteredBy { get; set; }
    }
}
