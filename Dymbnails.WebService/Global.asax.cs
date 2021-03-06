﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dymbnails.WebService {
    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{id}", new { controller = "Main", action = "Render", id = "" });
        }

        protected void Application_Start() {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}