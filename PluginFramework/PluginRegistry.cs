namespace PluginFramework
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public class PluginRegistry
    {
        private readonly List<IPlugin> plugins;

        private PluginRegistry()
        {
            this.plugins = new List<IPlugin>();
        }

        private static PluginRegistry current;

        public static PluginRegistry Current
        {
            get { return current ?? (current = new PluginRegistry()); }
        }

        internal void AddPlugin(IPlugin plugin)
        {
            this.plugins.Add(plugin);
        }

        public IReadOnlyList<IPlugin> Plugins => this.plugins.ToImmutableList();
    }
}