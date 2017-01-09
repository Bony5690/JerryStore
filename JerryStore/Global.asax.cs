using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JerryStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //TODO: Create the database for this and come back to patch it in
            //string connectionString = ConfigurationManager.ConnectionString["JerryStore"].ConnectionString;
            WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("Customers", "Customers", "ID", "EmailAddress", true);


        }
    }
}
