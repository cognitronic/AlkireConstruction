using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Infrastructure.Domain;
using RAM.Core.Domain.User;

namespace RAM.Core.Domain.Blog
{
    [Serializable]
    public class Blog : EntityBase, IBlog
    {
        #region IBlog Members
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual int BlogCategoryID { get; set; }
        [DataMember]
        public virtual string Post { get; set; }
        [DataMember]
        public virtual string PostPreview { get; set; }
        [DataMember]
        public virtual int EnteredBy { get; set; }
        [DataMember]
        public virtual string SEOKeywords { get; set; }
        [DataMember]
        public virtual bool IsActive { get; set; }
        [DataMember]
        public virtual string SEODescription { get; set; }
        [DataMember]
        public virtual DateTime DatePosted { get; set; }
        [DataMember]
        public virtual string ImagePath { get; set; }

        public virtual IUser EnteredByRef { get; set; }
        public virtual IBlogCategory Category { get; set; }

        private IList<BlogTag> _tags = new List<BlogTag>();
        [DataMember]
        public virtual IList<BlogTag> Tags { get { return _tags; } set { _tags = value; } }

        #endregion

        #region ISystemObject Members
        [DataMember]
        public virtual int ID { get; set; }
        [DataMember]
        public virtual Guid SystemID { get; set; }
        [DataMember]
        public virtual string Type { get; set; }

        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
