using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PluginFramework;
using PluginFramework.ViewEngine;

namespace PluggableMVC
{
    public class MvcApplication : PluggableApplication
    {
        protected override void OnApplicationStarting(object sender, EventArgs e)
        {
            base.OnApplicationStarting(sender, e);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}