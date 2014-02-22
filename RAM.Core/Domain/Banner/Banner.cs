using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.Banner
{
    [Serializable]
    public class Banner : EntityBase, IBanner
    {
        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual string ImagePath { get; set; }

        [DataMember]
        public virtual string AltText { get; set; }

        [DataMember]
        public virtual string SubText1 { get; set; }

        [DataMember]
        public virtual string SubText2 { get; set; }

        [DataMember]
        public virtual string Title { get; set; }


        [DataMember]
        public virtual Guid SystemID { get; set; }

        [DataMember]
        public virtual string Type { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
