using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.Messaging.Blog;
using RAM.Core.Domain.Blog;

namespace RAM.Services.Interfaces
{
    public interface ITagService
    {
        Tag GetByID(int tagID);
        Tag GetByName(string name);
        IList<Tag> GetAll();
        IList<Tag> GetForAutoComplete(string query);
        void SaveTag(Tag tag);
        void DeleteTag(Tag tag);
    }
}
