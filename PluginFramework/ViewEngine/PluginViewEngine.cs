using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

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
            var areaViewLocationFormats = new List<string>();
            var areaMasterLocationFormats = new List<string>();
            var areaPartialViewLocationFormats = new List<string>();
            var viewLocationFormats = new List<string>();
            var masterLocationFormats = new List<string>();
            var partialViewLocationFormats = new List<string>();

            foreach (var plugin in PluginRegistry.Current.Plugins)
            {
                areaViewLocationFormats.AddRange(new[] {
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/{{1}}/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/{{1}}/{{0}}.vbhtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/Shared/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/Shared/{{0}}.vbhtml"
                });

                areaMasterLocationFormats.AddRange(new[] {
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/{{1}}/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/{{1}}/{{0}}.vbhtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/Shared/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/Shared/{{0}}.vbhtml"
                });

                areaPartialViewLocationFormats.AddRange(new[] {
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/{{1}}/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/{{1}}/{{0}}.vbhtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/Shared/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Areas/{{2}}/Views/Shared/{{0}}.vbhtml"
                });

                viewLocationFormats.AddRange(new[] {
                    $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.vbhtml",
                    $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.vbhtml"
                });

                masterLocationFormats.AddRange(new[] {
                    $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.vbhtml",
                    $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.vbhtml"
                });

                partialViewLocationFormats.AddRange(new[] {
                    $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Views/{{1}}/{{0}}.vbhtml",
                    $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.cshtml",
                    $"~/Plugins/{plugin.Name}/Views/Shared/{{0}}.vbhtml"
                });
            }

            this.AreaViewLocationFormats = this.AreaViewLocationFormats.Concat(areaViewLocationFormats).ToArray();
            this.AreaMasterLocationFormats = this.AreaMasterLocationFormats.Concat(areaMasterLocationFormats).ToArray();
            this.AreaPartialViewLocationFormats = this.AreaPartialViewLocationFormats.Concat(areaPartialViewLocationFormats).ToArray();

            this.ViewLocationFormats = this.ViewLocationFormats.Concat(viewLocationFormats).ToArray();
            this.MasterLocationFormats = this.MasterLocationFormats.Concat(masterLocationFormats).ToArray();
            this.PartialViewLocationFormats = this.PartialViewLocationFormats.Concat(partialViewLocationFormats).ToArray();
        }
    }
}