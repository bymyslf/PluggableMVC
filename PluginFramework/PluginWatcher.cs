namespace PluginFramework
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public class PluginWatcher : Disposable
    {
        private static volatile bool isRestarting = false;
        private static readonly object @lock = new object();
        private readonly List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();

        public void Start(params string[] pluginFolders)
        {
            foreach (var folder in pluginFolders)
            {
                if (Directory.Exists(folder))
                {
                    var fsw = new FileSystemWatcher(folder, "*.dll")
                    {
                        IncludeSubdirectories = true,
                        NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                    };

                    watchers.Add(fsw);
                    fsw.Changed += FswChanged;
                    fsw.EnableRaisingEvents = true;
                }
            }
        }

        private void FswChanged(object sender, FileSystemEventArgs e)
        {
            //Ensure the app is not restarted multiple times for multiple saving during the same app domain execution
            if (isRestarting == false)
            {
                lock (@lock)
                {
                    if (isRestarting == false)
                    {
                        isRestarting = true;

                        HttpRuntime.UnloadAppDomain();
                        Dispose();
                    }
                }
            }
        }

        protected override void DisposeResources()
        {
            foreach (var fw in watchers)
            {
                fw.Dispose();
            }
        }
    }
}