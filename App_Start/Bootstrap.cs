using MDW.Repository;
using MDW.Repository.Interfaces;
using MDW.Services;
using MDW.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MDW
{
    public class Bootstrap
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.LowercaseUrls = true;
            RouteTable.Routes.AppendTrailingSlash = false;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Pages", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                .Include("~/scripts/jquery-{version}.js",
                         "~/scripts/jquery.validate*",
                         "~/scripts/modernizr-*",
                         "~/scripts/bootstrap.js",
                         "~/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/bootstrap.css",
                         "~/content/site.css"));

            bundles.Add(new StyleBundle("~/styles/theme-default")
                .Include("~/content/themes/site-theme-default.css"));

            bundles.Add(new StyleBundle("~/styles/theme-dark")
                .Include("~/content/themes/site-theme-dark.css"));

            bundles.Add(new StyleBundle("~/styles/theme-blue")
                .Include("~/content/themes/site-theme-blue.css"));

            bundles.Add(new StyleBundle("~/styles/theme-yellow")
                .Include("~/content/themes/site-theme-yellow.css"));
        }

        public static void RegisterDependencies()
        {
            var container = new Container();

            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IRoleRepository, RoleRepository>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IGroupRepository, GroupRepository>();
            container.Register<IPageRepository, PageRepository>();
            container.Register<IImageRepository, ImageRepository>();
            container.Register<IPolicyRepository, PolicyRepository>();

            container.Register<IRoleService, RoleService>();
            container.Register<IUserService, UserService>();
            container.Register<IMarkdownService, MarkdownService>();
            container.Register<IGroupService, GroupService>();
            container.Register<IPageService, PageService>();
            container.Register<IImageService, ImageService>();
            container.Register<IPolicyService, PolicyService>();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public static void RegisterDefaults(HttpApplication context)
        {
            if (!File.Exists(Path.Combine(ConfigurationManager.AppSettings["mdw.db.path"], @"mdw.db")))
            {
                var roles = DependencyResolver.Current.GetService<IRoleService>();

                if (roles != null)
                {
                    roles.CreateRole("administrator", true).GetAwaiter().GetResult();
                    roles.CreateRole("user", true).GetAwaiter().GetResult();

                    var users = DependencyResolver.Current.GetService<IUserService>();

                    if (users != null && !users.UserExists("administrator").GetAwaiter().GetResult())
                        users.CreateUser("Administrator", string.Empty, "admin", "root@localhost", "administrator");
                }

                var groups = DependencyResolver.Current.GetService<IGroupService>();

                if (groups != null)
                {
                    groups.CreateGroup("default", true).GetAwaiter().GetResult();

                    var pages = DependencyResolver.Current.GetService<IPageService>();

                    if (pages != null)
                    {
                        pages.CreatePage(string.Empty, "default").GetAwaiter().GetResult();

                        pages.UpdatePage("/", "Markdown", "default", File.ReadAllText(Path.Combine(context.Server.MapPath("/"), "markdown.txt"))).GetAwaiter().GetResult();
                    }

                    var images = DependencyResolver.Current.GetService<IImageService>();

                    if (images != null)
                        images.CreateImage("markdown.png", "default", File.ReadAllBytes(Path.Combine(context.Server.MapPath("/"), "images/markdown.png")));
                }

                var policies = DependencyResolver.Current.GetService<IPolicyService>();

                if (policies != null)
                    policies.CreatePolicy("administrator", "default", true).GetAwaiter().GetResult();
            }
        }
    }
}