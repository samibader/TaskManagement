using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SBC.TimeCards.Startup))]
namespace SBC.TimeCards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutofac(app);
            ConfigureAuth(app);
        }
    }
}
