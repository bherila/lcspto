using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lcspto_mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Content", "page/{id}", new { controller = "Content", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("NewsRoute", "news/{category}/{id}",
                new { controller = "News", action = "Index", category = "", id = UrlParameter.Optional },
                new { controller = "News" })
                .DataTokens["UseNamespaceFallback"] = false;

            // default route
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults, 
                , new { controller = "Home|Account|AdminForms|Calendar|Content|Error|Gallery|Shared|Sync" },
                new string[] { "lcspto_mvc.*" }
            ).DataTokens["UseNamespaceFallback"] = false;

            //routes.MapRoute("Error", "{*url}",
            //    new { controller = "Error", action = "404" }
            //);

        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.DefaultNamespaces.Add("lcspto_mvc.Controllers");
        }
    }
}