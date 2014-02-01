using System.Web.Mvc;
using System.Web.Routing;

namespace Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
                "Content",
                "content/{action}/{*path}",
                new {controller = "Content"}
                );

            routes.MapRoute(
                "Tag",
                "tag/{tag}",
                new {controller = "Tag", action = "Index"}
                );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {id = UrlParameter.Optional}
                );
        }
    }
}