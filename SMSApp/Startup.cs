using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMSApp.Startup))]
namespace SMSApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
