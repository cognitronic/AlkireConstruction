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
        public int ID { get; set; }

        public int Title { get; set; }

        public int Category { get; set; }

        public IList<string> ImagePaths { get; set; }

        public DateTime ProjectDate { get; set; }

        public string Description { get; set; }

        public string DefaultImagePath { get; set; }

        public DateTime DateCreated { get; set; }

        public int EnteredBy { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }


        public Guid SystemID { get; set; }

        public string Type { get; set; }
    }
}
