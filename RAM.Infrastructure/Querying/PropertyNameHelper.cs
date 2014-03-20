using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RAM.Infrastructure.Querying
{
    public static class PropertyNameHelper
    {
        /// <summary>
        /// Allows for the ease of adding new criterion to a query.  Example: aQuery.Add(new Criterion(PropertyNameHelper.ResolvePropertyName<Product>(p => p.Color.ID), id, CriteriaOperator.Equal));
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ResolvePropertyName<T>(Expression<Func<T, object>> expression)
        {
            var expr = expression.Body as MemberExpression;
            if (expr == null)
            {
                var u = expression.Body as UnaryExpression;
                expr = u.Operand as MemberExpression;
            }
            return expr.ToString().Substring(expr.ToString().IndexOf(".") + 1);
        }
    }
}
