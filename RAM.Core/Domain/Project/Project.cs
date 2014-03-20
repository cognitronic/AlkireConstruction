using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RAM.Infrastructure.Domain;
using RAM.Core.Domain.User;

namespace RAM.Core.Domain.Project
{
    [Serializable]
    public class Project : EntityBase, IProject
    {
        [DataMember]
        public virtual int ID { get; set; }
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual int Category { get; set; }
        [DataMember]
        private IList<ProjectImage> _images = new List<ProjectImage>();
        [DataMember]
        public virtual IList<ProjectImage> Images { get { return _images; } set { _images = value; } }

        [DataMember]
        public virtual DateTime ProjectDate { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual string DefaultImagePath { get; set; }
        [DataMember]
        public virtual DateTime DateCreated { get; set; }
        [DataMember]
        public virtual int EnteredBy { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        [DataMember]
        public virtual Guid SystemID { get; set; }
        [DataMember]
        public virtual string Type { get; set; }
    }
}
