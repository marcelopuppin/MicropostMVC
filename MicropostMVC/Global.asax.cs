﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MicropostMVC.Framework.Binder;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Security;

namespace MicropostMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthentication());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Pages", action = "Home", id = UrlParameter.Optional}
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //var rxBundle = new DynamicFolderBundle("rx", "rx*.js");
            //BundleTable.Bundles.Add(rxBundle);

            BundleTable.Bundles.RegisterTemplateBundles();
            //BundleTable.Bundles.EnableDefaultBundles();

            //ModelBinders.Binders.DefaultBinder = new BoRefModelBinder();
            ModelBinders.Binders.Add(typeof(BoRef), new BoRefModelBinder());
        }
    }
}