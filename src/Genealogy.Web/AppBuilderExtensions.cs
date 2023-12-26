using Genealogy.Api.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Genealogy.Api;

internal static class AppBuilderExtensions
{
    public static THostBuilder AddAuth<THostBuilder>(this THostBuilder builder)
        where THostBuilder : IHostApplicationBuilder
    {
        builder.Services
               .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                   options.SlidingExpiration = true;
                   options.AccessDeniedPath = "/Forbidden/";
                   options.LoginPath = "/auth/login";
               });

        builder.Services
               .AddAuthorizationBuilder()
               .AddPolicy(Policies.CanRead, builder =>
               {
                   builder.RequireAuthenticatedUser()
                          .RequireRole(Roles.Reader);
               });

        return builder;
    }

    public static TAppBuilder UseAuth<TAppBuilder>(this TAppBuilder app) where TAppBuilder : IApplicationBuilder
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}