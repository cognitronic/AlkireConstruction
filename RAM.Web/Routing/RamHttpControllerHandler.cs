using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.WebHost;
using System.Web.SessionState;
using System.Web.Routing;

namespace RAM.Web.Routing
{
    public class RamHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public RamHttpControllerHandler(RouteData routeData)
            : base(routeData)
        {
        }
    }
}
