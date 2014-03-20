using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Session
{
    public class SessionManager
    {
        private static SessionManager _sessionManager = new SessionManager();
        private static ISessionProvider _session;

        private SessionManager()
        {
        }

        public static ISessionProvider Current
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }
    }
}
