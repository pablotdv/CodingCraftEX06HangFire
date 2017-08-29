using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Infraestrura.Filtros
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            bool boolAuthorizeCurrentUserToAccessHangFireDashboard = false;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Administradores"))
                    boolAuthorizeCurrentUserToAccessHangFireDashboard = true;
            }

            return boolAuthorizeCurrentUserToAccessHangFireDashboard;
        }
    }
}