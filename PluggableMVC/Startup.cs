using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PluggableMVC.Startup))]
namespace PluggableMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
