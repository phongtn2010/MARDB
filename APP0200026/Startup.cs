using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APP0200026.Startup))]
namespace APP0200026
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
