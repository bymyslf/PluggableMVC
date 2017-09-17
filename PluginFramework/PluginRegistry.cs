namespace PluginFramework
{
    using System.Collections.Generic;

    public class PluginRegistry
    {
        private PluginRegistry()
        {
            this.Plugins = new List<IPlugin>();
        }

        private static PluginRegistry current;

        public static PluginRegistry Current
        {
            get { return current ?? (current = new PluginRegistry()); }
        }

        public List<IPlugin> Plugins { get; private set; }
    }
}