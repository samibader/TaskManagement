using Hangfire;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(SBC.TimeCards.Startup))]
namespace SBC.TimeCards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutofac(app);
            ConfigureAuth(app);

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

            //RecurringJob.AddOrUpdate(() => Console.Write("Recurring"), "*/1 * * * *");
            
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
