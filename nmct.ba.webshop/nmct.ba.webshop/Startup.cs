using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ba.webshop.Startup))]
namespace nmct.ba.webshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
