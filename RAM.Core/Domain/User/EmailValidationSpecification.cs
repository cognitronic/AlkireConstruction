using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;
using System.Text.RegularExpressions;

namespace RAM.Core.Domain.User
{
    public class EmailValidationSpecification : ISpecification<IUser>
    {
        private static Regex _emailregex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        public bool IsSatisfiedBy(IUser candidate)
        {
            return _emailregex.IsMatch(candidate.Email);
        }

    }
}
