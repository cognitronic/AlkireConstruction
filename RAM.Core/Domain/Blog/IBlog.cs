using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using RAM.Core.Domain.User;

namespace RAM.Core.Domain.Blog
{
    public interface IBlog : ISystemObject
    {
        string Title { get; set; }
        int BlogCategoryID { get; set; }
        string Post { get; set; }
        string PostPreview { get; set; }
        int EnteredBy { get; set; }
        string SEOKeywords { get; set; }
        string SEODescription { get; set; }
        DateTime DatePosted { get; set; }
        bool IsActive { get; set; }
        string ImagePath { get; set; }
        IUser EnteredByRef { get; set; }
        IBlogCategory Category { get; set; }

        IList<BlogTag> Tags { get; set; }
    }
}
