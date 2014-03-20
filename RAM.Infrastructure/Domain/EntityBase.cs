using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RAM.Infrastructure.Domain
{
    [Serializable]
    public abstract class EntityBase : IAggregateRoot
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        public EntityBase()
        {

        }

        protected abstract void Validate();

        public virtual IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected virtual void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is EntityBase
                && this == (EntityBase)entity;
        }

        public override int GetHashCode()
        {
            //if (this.ID != null)
            //    return this.ID.GetHashCode();
            //else
            return base.GetHashCode() * 17;
        }

        //public static bool operator ==(EntityBase entity1, EntityBase entity2)
        //{
        //    if ((object)entity1 == null && (object)entity2 == null)
        //    {
        //        return true;
        //    }

        //    if ((object)entity1 == null || (object)entity2 == null)
        //    {
        //        return false;
        //    }

        //    if (entity1..ID.ToString() == entity2.ID.ToString())
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public static bool operator !=(EntityBase entity1, EntityBase entity2)
        //{
        //    return (!(entity1 == entity2));
        //}
    }
}
