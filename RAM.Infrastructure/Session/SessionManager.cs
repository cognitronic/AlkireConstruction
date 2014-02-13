using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaSeed.Core;

namespace RAM.Infrastructure.Session
{
    public class SessionManager
    {
        private static SessionManager _sessionManager = new SessionManager();
        private static IdeaSeed.Core.ISessionProvider _session;

        private SessionManager()
        {
        }

        public static IdeaSeed.Core.ISessionProvider Current
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
