namespace Plugin.Test
{
    using System;
    using PluginFramework;

    public class TestPlugin : IPlugin
    {
        public string EntryControllerName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Initialize()
        {
        }
    }
}