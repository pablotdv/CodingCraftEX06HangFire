using CodingCraftEX06HangFire.Infraestrura.BackgroundJobs;
using CodingCraftEX06HangFire.Infraestrura.Filtros;
using Hangfire;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire
{
    public partial class Startup
    {
        public void ConfigureHangFire(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
               .UseSqlServerStorage("DefaultConnection");

            var options = new DashboardOptions
            {
                AppPath = VirtualPathUtility.ToAbsolute("~/"),
                Authorization = new[] { new HangFireAuthorizationFilter() }
            };

            app.UseHangfireDashboard("/HangFire", options);
            app.UseHangfireServer();

            Hangfire.BackgroundJob.Enqueue(() => AcoesJobs.MecanicaJob());
            RecurringJob.RemoveIfExists("acoes-mecanica");           
            
        }
    }
}