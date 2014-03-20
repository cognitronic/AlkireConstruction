using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RAM.Infrastructure.Querying
{
    public class Criterion
    {
        private string _propertyName;
        private object _value;
        private CriteriaOperator _criteriaOperator;

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }

        public CriteriaOperator CriteriaOperator
        {
            get
            {
                return _criteriaOperator;
            }
        }
        
        public Criterion(string propertyName, object value, CriteriaOperator criteriaOperator)
        {
            _propertyName = propertyName;
            _value = value;
            _criteriaOperator = criteriaOperator;
        }

        /// <summary>
        /// Allows for the ease of creating Criterion objects.  Example: aQuery.Add(Criterion.Create<Product>(p => p.Color.ID, id, CriteriaOperator.Equal))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="criteriaOperator"></param>
        /// <returns></returns>
        public static Criterion Create<T>(Expression<Func<T, object>> expression, object value, CriteriaOperator criteriaOperator)
        {
            string propertyName = PropertyNameHelper.ResolvePropertyName<T>(expression);
            Criterion myCriterion = new Criterion(propertyName, value, criteriaOperator);
            return myCriterion;
        }
    }
}
