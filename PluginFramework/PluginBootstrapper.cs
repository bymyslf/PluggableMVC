namespace PluginFramework
{
    public static class PluginBootstrapper
    {
        static PluginBootstrapper()
        {
        }

        public static void Initialize()
        {
            foreach (var plugin in PluginRegistry.Current.Plugins)
            {
                plugin.Initialize();
            }
        }
    }
}