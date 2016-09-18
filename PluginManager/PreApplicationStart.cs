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

    public class PreApplicationStart
    {
        static PreApplicationStart()
        {
            string pluginsPath = HostingEnvironment.MapPath("~/plugins");
            string pluginsTempPath = HostingEnvironment.MapPath("~/plugins/temp");

            if (pluginsPath == null || pluginsTempPath == null)
            {
                throw new DirectoryNotFoundException("plugins");
            }

            PluginFolder = new DirectoryInfo(pluginsPath);
            ShadowCopyFolder = new DirectoryInfo(pluginsTempPath);
        }

        /// <summary>
        /// The source plugin folder from which to copy from
        /// </summary>
        /// <remarks>
        /// This folder can contain sub folders to organize plugin types
        /// </remarks>
        private static readonly DirectoryInfo PluginFolder;

        /// <summary>
        /// The folder to  copy the plugin DLLs to use for running the app
        /// </summary>
        private static readonly DirectoryInfo ShadowCopyFolder;

        /// <summary>
        /// Initialize method that registers all plugins
        /// </summary>
        public static void InitializePlugins()
        {
            Directory.CreateDirectory(ShadowCopyFolder.FullName);
            foreach (var dll in ShadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    dll.Delete();
                }
                catch (Exception)
                {
                }
            }

            //copy files
            foreach (var plug in PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
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
            var assemblies = ShadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                    .Select(x => AssemblyName.GetAssemblyName(x.FullName))
                    .Select(x => Assembly.Load(x.FullName));

            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().Where(t => t.GetInterface(typeof(IPlugin).Name) != null).FirstOrDefault();
                if (type != null)
                {
                    //Add the plugin as a reference to the application
                    BuildManager.AddReferencedAssembly(assembly);

                    ////Add the modules to the PluginManager to manage them later
                    //var module = (IModule)Activator.CreateInstance(type);
                    //PluginManager.Current.Modules.Add(module, assembly);
                }
            }
        }
    }
}