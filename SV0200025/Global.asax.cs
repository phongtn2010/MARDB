using DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SV0200025
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Connection.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Globals.PageSize = 10;

            Globals.api_bantinxml25 = ConfigurationManager.AppSettings["api_bantinxml25"];
            Globals.api_bantinxml26 = ConfigurationManager.AppSettings["api_bantinxml26"];

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
