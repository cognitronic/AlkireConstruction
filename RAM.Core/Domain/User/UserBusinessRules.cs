using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.User
{
    public class UserBusinessRules
    {
        public static readonly BusinessRule FirstNameRequired = new BusinessRule("FirstName", "A user must have a first name");
        public static readonly BusinessRule LastNameRequired = new BusinessRule("LastName", "A user must have a last name");
        public static readonly BusinessRule EmailRequired = new BusinessRule("Email", "A user must have a valid email address");
        public static readonly BusinessRule EmailUnique = new BusinessRule("Email", "A user must have a unique email address.  This email is already in use.");
        public static readonly BusinessRule IdentityTokenRequired = new BusinessRule("IdentityToken", "A user must have an identity token");
    }
}
