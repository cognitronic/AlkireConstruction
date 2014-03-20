using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RAM.Web.Routing;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace RAM.Admin.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*browserlink}", new { browserlink = @".*/arterySignalR/ping" });

            var route = routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { action = "Get", id = RouteParameter.Optional }
              );
            route.RouteHandler = new RamHttpControllerRouteHandler();

        //    routes.Add(
        //    new Route("Blog/{id}",
        //        new RouteValueDictionary(
        //            new
        //            {
        //                controller = "Blog",
        //                action = "ByTitle"
        //            }),
        //            new HyphenatedRouteHandler())
        //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            ((DefaultContractResolver)GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver).IgnoreSerializableAttribute = true;
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
    }
}
