using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Querying;
using NHibernate;
using NHibernate.Criterion;

namespace RAM.Repository.NHibernate
{
    public static class QueryTranslator
    {
        public static ICriteria TranslateIntoNHQuery<T>(this Query query, ICriteria criteria)
        {
            BuildQueryFrom(query, criteria);
            if (query.OrderByProperty != null)
                criteria.AddOrder(new Order(query.OrderByProperty.PropertyName, !query.OrderByProperty.Desc));
            return criteria;
        }

        private static void BuildQueryFrom(Query query, ICriteria criteria)
        {
            IList<ICriterion> criterions = new List<ICriterion>();
            if (query.Criteria != null)
            {
                foreach (var c in query.Criteria)
                {
                    ICriterion criterion;
                    switch (c.CriteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            criterion = Expression.Eq(c.PropertyName, c.Value);
                            break;
                        case CriteriaOperator.LesserThanOrEqual:
                            criterion = Expression.Le(c.PropertyName, c.Value);
                            break;
                        case CriteriaOperator.Between:
                            criterion = Expression.Between(c.PropertyName, c.Value.ToString().Split(',')[0], c.Value.ToString().Split(',')[1]);
                            break;
                        case CriteriaOperator.Like:
                            criterion = Expression.Like(c.PropertyName, c.Value);
                            break;
                        case CriteriaOperator.GreaterThanOrEqual:
                            criterion = Expression.Ge(c.PropertyName, c.Value);
                            break;
                        default:
                            throw new ApplicationException("No operator defined");
                    }
                    criterions.Add(criterion);
                }

                if (query.QueryOperator == QueryOperator.And)
                {
                    Conjunction andSubQuery = Expression.Conjunction();
                    foreach (var criterion in criterions)
                    {
                        andSubQuery.Add(criterion);
                    }
                    criteria.Add(andSubQuery);
                }
                else
                {
                    Disjunction orSubQuery = Expression.Disjunction();
                    foreach (var criterion in criterions)
                    {
                        orSubQuery.Add(criterion);
                    }
                    criteria.Add(orSubQuery);
                }
                foreach (var sub in query.SubQueries)
                {
                    BuildQueryFrom(sub, criteria);
                }
            }
        }
    }
}
