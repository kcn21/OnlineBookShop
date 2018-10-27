using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookShop2.Startup))]
namespace BookShop2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
