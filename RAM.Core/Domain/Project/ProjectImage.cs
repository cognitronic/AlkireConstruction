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
        public int ID { get; set; }
        public int ProjectID { get; set; }

        public string ImagePath { get; set; }

        public string AltText { get; set; }


        public Guid SystemID { get; set; }

        public string Type { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
