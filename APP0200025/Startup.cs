using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APP0200025.Startup))]
namespace APP0200025
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);   //Co chinh sau bao nhieu phut thi huy Cookies dang nhap lai trong Startup.Auth.cs
        }
    }
}
