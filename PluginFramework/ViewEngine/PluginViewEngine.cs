using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PluginFramework.ViewEngine
{
    public class PluginViewEngine : RazorViewEngine
    {
        public PluginViewEngine()
        {
            this.ExpandViewLocations();
        }

        private void ExpandViewLocations()
        {
            var existingViewPaths = new List<string>(ViewLocationFormats);
            var existingPartialViewPaths = new List<string>(PartialViewLocationFormats);

            foreach (var plugin in PluginRegistry.Current.Plugins)
            {
                var viewsPath = $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.cshtml";
                var sharedViewsPath = $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.cshtml";

                existingViewPaths.Add(viewsPath);
                existingViewPaths.Add(sharedViewsPath);

                existingPartialViewPaths.Add(viewsPath);
                existingPartialViewPaths.Add(sharedViewsPath);
            }

            this.ViewLocationFormats = existingViewPaths.ToArray();
            this.PartialViewLocationFormats = existingPartialViewPaths.ToArray();
        }
    }
}