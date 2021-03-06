using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VietNamUSA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "VietNamUSA.Controllers" }
           );
        }
    }
}
