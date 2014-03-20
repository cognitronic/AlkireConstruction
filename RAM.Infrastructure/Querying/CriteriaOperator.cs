using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Querying
{
    public enum CriteriaOperator
    {
        Equal,
        LesserThanOrEqual,
        GreaterThanOrEqual,
        Like,
        Between,
        NotApplicable
    }
}
