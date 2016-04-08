using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DEMO_CrystalReport.Startup))]
namespace DEMO_CrystalReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
