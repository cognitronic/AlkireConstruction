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
    public class BlogTag : EntityBase, IBlogTag
    {
        [DataMember]
        public virtual int BlogID { get; set; }

        [DataMember]
        public virtual int TagID { get; set; }

        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual Guid SystemID { get; set; }

        [DataMember]
        public virtual string Type { get; set; }
        [DataMember]
        public virtual Blog BlogRef { get; set; }
        [DataMember]
        public virtual Tag TagRef { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
