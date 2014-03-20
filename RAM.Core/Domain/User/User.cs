using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace RAM.Core.Domain.User
{
    [Serializable]
    [DataContract]
    public class User : EntityBase, IUser
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(FirstName))
                base.AddBrokenRule(UserBusinessRules.FirstNameRequired);
            if (string.IsNullOrEmpty(LastName))
                base.AddBrokenRule(UserBusinessRules.LastNameRequired);
            if (string.IsNullOrEmpty(Email))
                base.AddBrokenRule(UserBusinessRules.EmailRequired);
            if (!new EmailValidationSpecification().IsSatisfiedBy(this))
                base.AddBrokenRule(UserBusinessRules.EmailRequired);
        }

        #region IUser Members

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual string Password { get; set; }

        [DataMember]
        public virtual string PasswordQuestion { get; set; }

        [DataMember]
        public virtual string PasswordAnswer { get; set; }

        [DataMember]
        public virtual DateTime LastLoginDate { get; set; }

        [DataMember]
        public virtual int AccessLevel { get; set; }

        #endregion

        #region IBaseUser Members

        [DataMember]
        public virtual string Email { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        #endregion

        #region IAuditable Members

        [DataMember]
        public virtual int EnteredBy { get; set; }

        [DataMember]
        public virtual int ChangedBy { get; set; }

        [DataMember]
        public virtual DateTime DateCreated { get; set; }

        [DataMember]
        public virtual DateTime LastUpdated { get; set; }

        #endregion

        #region ISystemObject Members

        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual Guid SystemID { get; set; }

        [DataMember]
        public virtual string Type { get; set; }

        #endregion

        public User()
        {
            this.Type = "User";
        }

        public virtual string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
