namespace PluginFramework
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using ViewEngine;

    public abstract class PluggableApplication : HttpApplication
    {
        private readonly PluginWatcher pluginWatcher = new PluginWatcher();

        protected void Application_Start(object sender, EventArgs e)
        {
            this.OnApplicationStart(sender, e);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            this.OnApplicationEnd(sender, e);
        }

        protected virtual void OnApplicationStart(object sender, EventArgs e)
        {
            PluginBootstrapper.Initialize();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new PluginViewEngine());

            var pluginFolder = HostingEnvironment.MapPath("~/Plugins");
            var pluginFolders = Directory.GetDirectories(pluginFolder)
                  .Where(dir => !dir.EndsWith("Temp")).ToArray();

            this.pluginWatcher.Start(pluginFolders);
        }

        protected virtual void OnApplicationEnd(object sender, EventArgs e)
        {
            pluginWatcher.Dispose();
        }
    }
}