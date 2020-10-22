﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APP0200025
{
    public static class WebApiConfig
    {
        public static IContainer Container { get; set; }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
