using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RAM.Infrastructure.Domain
{
    [Serializable]
    public class BusinessRule
    {
        private string _property;
        private string _rule;

        public string Property
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }

        public string Rule
        {
            get
            {
                return _rule;
            }
            set
            {
                _rule = value;
            }
        }

        public BusinessRule(string property, string rule)
        {
            this._property = property;
            this._rule = rule;
        }

    }
}
