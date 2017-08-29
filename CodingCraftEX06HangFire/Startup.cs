using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraftEX06HangFire.Startup))]
namespace CodingCraftEX06HangFire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureHangFire(app);
        }
    }
}
