namespace PluginFramework
{
    public interface IPlugin
    {
        /// <summary>
        /// Title of the plugin, can be used as a property to display on the user interface
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Name of the plugin, should be an unique name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Entry controller name
        /// </summary>
        string EntryControllerName { get; }

        /// <summary>
        /// Initialize the plugin with all the scripts, css and DI
        /// </summary>
        void Initialize();
    }
}