using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using MediaSwap.Core.Repositories;

namespace MediaSwap.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Search", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;            

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}