using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using RAM.Core.Domain.User;

namespace RAM.Core.Domain.Project
{
    public interface IProjectImage : ISystemObject
    {
        int ID { get; set; }
        int ProjectID { get; set; }
        string ImagePath { get; set; }
        string AltText { get; set; }
    }
}
