using System.Data.Entity;
using ContactLibrary.Data.DataContext;
using System.Web.Mvc;
using System.Web.Routing;
using ContactLibrary.Web.App_Start;
using System.Web.Optimization;

namespace ContactLibrary.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlDbContext>());

            using (var con = new SqlDbContext())
            {
                con.Database.Initialize(true);
                con.Database.CreateIfNotExists();
            }

        }
    }
}
