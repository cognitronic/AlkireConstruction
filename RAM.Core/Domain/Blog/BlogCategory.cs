using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.Blog
{
    [Serializable]
    public class BlogCategory : EntityBase, IBlogCategory
    {
        #region IBlogCategory Members
        [DataMember]
        public virtual string Name { get; set; }

        public virtual IList<IBlog> Posts { get; set; }

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
