using System;
using Hangfire.Dashboard;
using System.Security.Claims;
using System.Diagnostics.CodeAnalysis;
using AnimeMovie.Entites;

namespace AnimeMovie.API
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static readonly string HangFireCookieName = "HangFireCookie";
        private static readonly int CookieExpirationMinutes = 60;
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpcontext = context.GetHttpContext();
            var access_token = String.Empty;
            var setCookie = false;
            var rol = httpcontext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (httpcontext.Request.Query.ContainsKey("t"))
            {
                access_token = httpcontext.Request.Query["t"].FirstOrDefault();
                setCookie = true;
            }
            else
            {
                access_token = httpcontext.Request.Cookies[HangFireCookieName];
            }
            if (setCookie)
            {
                httpcontext.Response.Cookies.Append(HangFireCookieName,
                access_token,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(CookieExpirationMinutes)
                });
            }
            if (access_token == null && rol == "Admin")
            {
                httpcontext.Response.Redirect("/login.html");
            }
            return true;
        }
    }
}

