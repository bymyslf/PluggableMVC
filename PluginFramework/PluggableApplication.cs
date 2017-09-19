namespace PluginFramework
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using ViewEngine;

    public abstract class PluggableApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            this.OnApplicationStarting(sender, e);
        }

        protected virtual void OnApplicationStarting(object sender, EventArgs e)
        {
            PluginBootstrapper.Initialize();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new PluginViewEngine());
        }
    }
}