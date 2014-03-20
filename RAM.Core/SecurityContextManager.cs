using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Core
{
    public class SecurityContextManager
    {
        private static IRAMSecurityContext _securityContext;

        private SecurityContextManager() { }

        public static IRAMSecurityContext Current
        {
            get
            {
                return _securityContext;
            }
            set
            {
                _securityContext = value;
            }
        }
    }
}
