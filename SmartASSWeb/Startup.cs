using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartASSWeb.Startup))]
namespace SmartASSWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
