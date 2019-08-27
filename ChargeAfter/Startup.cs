using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChargeAfter.Startup))]
namespace ChargeAfter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
