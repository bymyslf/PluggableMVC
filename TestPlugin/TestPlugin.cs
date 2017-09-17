namespace TestPlugin
{
    using System;
    using PluginFramework;

    public class TestPlugin : IPlugin
    {
        public string EntryControllerName
        {
            get
            {
                return "TestPlugin";
            }
        }

        public string Name
        {
            get
            {
                return "TestPlugin";
            }
        }

        public string Title
        {
            get
            {
                return "Test Plugin Controller";
            }
        }

        public void Initialize()
        {
        }
    }
}