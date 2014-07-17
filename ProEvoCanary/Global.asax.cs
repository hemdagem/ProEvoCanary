using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ProEvo45;
using ProEvo45.App_Start;

namespace ProEvoCanary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
