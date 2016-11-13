using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDV.Startup))]
namespace WebDV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
