using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProcessAdminApp
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

        //    routes.MapRoute(
        //        "Company",
        //        "Define/{PQpara}",
        //        new { Controller = "Company", action = "Define" },
        //new { entryDate = @"\d{2}-\d{2}-\d{4}" });
        
        }




    }

    //public class CmsUrlConstraint : IRouteConstraint
    //{
    //    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    //    {
    //        var db = new Models.rout();
    //        if (values[parameterName] != null)
    //        {
    //            var permalink = values[parameterName].ToString();
    //            return db.CMSPages.Any(p => p.Permalink == permalink);
    //        }
    //        return false;
    //    }
    //}

//    routes.MapRoute(
//    name: "CmsRoute",
//    url: "{*permalink}",
//    defaults: new {controller = "Page", action = "Index"},
//    constraints: new { permalink = new CmsUrlConstraint() }
//);
}