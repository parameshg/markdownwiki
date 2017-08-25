using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MDW
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Bootstrap.RegisterFilters(GlobalFilters.Filters);
            Bootstrap.RegisterRoutes(RouteTable.Routes);
            Bootstrap.RegisterBundles(BundleTable.Bundles);
            Bootstrap.RegisterDependencies();
            Bootstrap.RegisterDefaults();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}