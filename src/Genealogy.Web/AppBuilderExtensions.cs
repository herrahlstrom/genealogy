using Genealogy.Api.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Genealogy.Api;

internal static class AppBuilderExtensions
{


    public static TAppBuilder UseAuth<TAppBuilder>(this TAppBuilder app) where TAppBuilder : IApplicationBuilder
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}