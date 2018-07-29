using ContactLibrary.Web.DependencyResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContactLibrary.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
