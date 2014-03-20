using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using RAM.Core.Domain.User;

namespace RAM.Core.Domain.Banner
{
    public interface IBanner : ISystemObject
    {
        int ID { get; set; }
        string ImagePath { get; set; }
        string AltText { get; set; }
        string SubText1 { get; set; }
        string SubText2 { get; set; }
        string Title { get; set; }
    }
}
