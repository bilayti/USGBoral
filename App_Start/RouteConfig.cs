using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "Home",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "HomePage", action = "Home", id = 1 }
             );

            routes.MapRoute(
                 name: "UserReg",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "UserRegistration", action = "UserReg", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                 name: "AddUserReg",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "AddUserRegistration", action = "AddUserReg", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "UserType",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "HomePage", action = "UserType", id = UrlParameter.Optional }
             );
        }
    }
}