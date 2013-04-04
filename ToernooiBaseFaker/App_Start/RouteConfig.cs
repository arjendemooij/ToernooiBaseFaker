using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ToernooiBaseFaker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ToernooiBasePage",
                url: "{controller}/{action}/{relativeUrl}",
                defaults: new { controller = "Home", action = "ToernooiBasePage", relativeUrl = "/opvraag/toernooienprov.php?id=1&se=14" }
            );
        }
    }
}