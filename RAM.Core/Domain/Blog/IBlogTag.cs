using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.Blog
{
    public interface IBlogTag : ISystemObject
    {
        int BlogID { get; set; }
        int TagID { get; set; }

        Blog BlogRef { get; set; }
        Tag TagRef { get; set; }
    }
}
