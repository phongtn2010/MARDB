using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DomainModel;

namespace APP0200025
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Connection.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Globals.PageSize = 30;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
