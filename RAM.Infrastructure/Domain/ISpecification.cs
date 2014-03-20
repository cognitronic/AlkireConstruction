using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Domain
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}
