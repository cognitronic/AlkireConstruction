using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using System.Runtime.Serialization;

namespace RAM.Core.Domain.Subscriber
{
    [Serializable]
    public class Subscriber : EntityBase, ISubscriber
    {
        [DataMember]
        public virtual string Email { get; set; }
        [DataMember]
        public virtual string FirstName { get; set; }
        [DataMember]
        public virtual string LastName { get; set; }
        [DataMember]
        public virtual string Phone { get; set; }
        [DataMember]
        public virtual DateTime DateCreated { get; set; }
        [DataMember]
        public virtual int ID { get; set; }
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
