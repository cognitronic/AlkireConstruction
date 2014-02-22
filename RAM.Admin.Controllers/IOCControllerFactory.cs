using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using System.Web.Mvc;
using System.Web.Routing;
using RAM.Web.Security;
using RAM.Core;
using RAM.Infrastructure.Session;

namespace RAM.Admin.Controllers
{
    public class IOCControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (SecurityContextManager.Current == null)
                SecurityContextManager.Current = new WebSecurityContext();
            if (SessionManager.Current == null)
                SessionManager.Current = new WebSessionProvider();
            return ObjectFactory.GetInstance(controllerType) as IController;
        }
    }
}
