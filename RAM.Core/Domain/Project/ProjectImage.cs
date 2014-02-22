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
    public class ProjectImage : EntityBase, IProjectImage
    {
        public ProjectImage() { }
        public ProjectImage(int projectID, string imagePath, string altText)
        {
            this.ProjectID = projectID;
            this.ImagePath = imagePath;
            this.AltText = altText;
        }
        [DataMember]
        public virtual int ID { get; set; }
        [DataMember]
        public virtual int ProjectID { get; set; }
        [DataMember]
        public virtual string ImagePath { get; set; }
        [DataMember]
        public virtual string AltText { get; set; }
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
