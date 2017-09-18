using System.Web;

[assembly: PreApplicationStartMethod(typeof(PluginFramework.PreApplicationStart), "InitializePlugins")]

namespace PluginFramework
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;
    using System.Web.Hosting;

    //https://shazwazza.com/post/developing-a-plugin-framework-in-aspnet-with-medium-trust/
    public class PreApplicationStart
    {
        private const string DllExtension = "*.dll";

        private static readonly DirectoryInfo PluginFolder;
        private static readonly DirectoryInfo ShadowCopyFolder;

        static PreApplicationStart()
        {
            string pluginsPath = HostingEnvironment.MapPath("~/Plugins");
            string pluginsShadowPath = HostingEnvironment.MapPath("~/Plugins/Temp");

            if (pluginsPath == null || pluginsShadowPath == null)
            {
                throw new DirectoryNotFoundException("Plugins");
            }

            PluginFolder = new DirectoryInfo(pluginsPath);
            ShadowCopyFolder = new DirectoryInfo(pluginsShadowPath);
        }

        public static void InitializePlugins()
        {
            Directory.CreateDirectory(ShadowCopyFolder.FullName);

            foreach (var dll in ShadowCopyFolder.GetFiles(DllExtension, SearchOption.AllDirectories))
            {
                try
                {
                    dll.Delete();
                }
                catch (Exception)
                {
                }
            }

            foreach (var plug in PluginFolder.GetFiles(DllExtension, SearchOption.AllDirectories))
            {
                try
                {
                    var di = Directory.CreateDirectory(ShadowCopyFolder.FullName);
                    File.Copy(plug.FullName, Path.Combine(di.FullName, plug.Name), true);
                }
                catch (Exception)
                {
                }
            }

            // * This will put the plugin assemblies in the 'Load' context
            // This works but requires a 'probing' folder be defined in the web.config
            // eg: <probing privatePath="plugins/temp" />
            var assemblies = ShadowCopyFolder.GetFiles(DllExtension, SearchOption.AllDirectories)
                    .Select(x => AssemblyName.GetAssemblyName(x.FullName))
                    .Select(x => Assembly.Load(x.FullName));

            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().Where(t => t.GetInterface(typeof(IPlugin).Name) != null).FirstOrDefault();
                if (type != null)
                {
                    BuildManager.AddReferencedAssembly(assembly);

                    var plugin = (IPlugin)Activator.CreateInstance(type);
                    PluginRegistry.Current.AddPlugin(plugin);
                }
            }
        }
    }
}