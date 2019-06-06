﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookReadingEvent.Web
{
    public class RouteConfig
    {
        public class HyphenatedRouteHandler : MvcRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                requestContext.RouteData.Values["controller"] = requestContext.RouteData.Values["controller"].ToString().Replace("-", "");
                requestContext.RouteData.Values["action"] = requestContext.RouteData.Values["action"].ToString().Replace("-", "");
                return base.GetHttpHandler(requestContext);
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ).RouteHandler = new HyphenatedRouteHandler();
        }
    }
}
