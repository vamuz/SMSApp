using System;
using Hangfire;
using Microsoft.Owin;
using Owin;
using SMSApp.Controllers;

[assembly: OwinStartupAttribute(typeof(SMSApp.Startup))]
namespace SMSApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

          app.UseHangfireDashboard();
            //SMSAppRegistrationsController obj= new SMSAppRegistrationsController();

            //RecurringJob.AddOrUpdate(() => obj.PushSMS(), Cron.Minutely);

            app.UseHangfireServer();
        }
    }
}
