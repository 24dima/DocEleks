using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDocEleks.Startup))]
namespace WebDocEleks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
