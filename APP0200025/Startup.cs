using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APP0200025.Startup))]
namespace APP0200025
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
