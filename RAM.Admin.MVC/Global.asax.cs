using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RAM.Infrastructure.Configuration;
using RAM.Infrastructure.Logging;
using RAM.Infrastructure.Email;
using RAM.Admin.Controllers;
using RAM.Admin.Controllers.Controllers;
using StructureMap;
using RAM.Repository.NHibernate.Repositories;
using RAM.Infrastructure.UnitOfWork;
using RAM.Repository.NHibernate;
using RAM.Services.Cache;

namespace RAM.Admin.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


            GlobalConfiguration.Configuration.DependencyResolver = new WebApiContrib.IoC.StructureMap.StructureMapResolver(BootStrapper.ConfigureStructureMapWebAPI());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootStrapper.ConfigureStructureMap();


            Services.AutoMapperBootStrapper.ConfigureAutoMapper();
            RAM.Admin.Controllers.AutoMapperBootStrapper.ConfigureAutoMapper();

            LoggingFactory.InitializeLogFactory(ObjectFactory.GetInstance<ILogger>());

            EmailServiceFactory.InitializeEmailServiceFactory
                                  (ObjectFactory.GetInstance<IEmailService>());

            ControllerBuilder.Current.SetControllerFactory(new IOCControllerFactory());

            LoggingFactory.GetLogger().Log("Application Started");
            ModelBinders.Binders.DefaultBinder = new App_Start.GenericModelBinder();
        }
    }
}
