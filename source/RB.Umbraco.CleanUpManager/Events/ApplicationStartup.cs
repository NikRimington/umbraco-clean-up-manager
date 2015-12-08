using System.Web.Mvc;
using System.Web.Routing;
using RB.Umbraco.CleanUpManager.Helpers;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core;

namespace RB.Umbraco.CleanUpManager.Events
{
    public class ApplicationStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteTable.Routes.MapRoute(
                "CleanUpManager",
                "App_Plugins/RB.Umbraco.CleanUpManager/{action}/{id}",
                new {controller = "CleanUpManager", action = "Resource", id = UrlParameter.Optional});

            //GlobalConfiguration.Configuration.Filters.Add(new PagedListActionFilterAttribute());

        }        

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Install
            DashboardHelper.EnsureTab("StartupDeveloperDashboardSection", "CleanUp Manager", "/App_Plugins/RB.Umbraco.CleanUpManager/Index.html");

            // Uninstall
            InstalledPackage.BeforeDelete += delegate
            {
                DashboardHelper.RemoveTab("StartupDeveloperDashboardSection", "CleanUp Manager");
            };
        }
    }
}