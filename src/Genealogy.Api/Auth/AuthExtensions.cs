using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Genealogy.Api.Auth;

internal static class AuthExtensions
{
    public static TAppBuilder UseAuth<TAppBuilder>(this TAppBuilder app) where TAppBuilder : IApplicationBuilder
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    public static THostBuilder AddAuth<THostBuilder>(this THostBuilder builder)
        where THostBuilder : IHostApplicationBuilder
    {

        builder.Services
               .AddAuthentication()
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        builder.Services
               .AddAuthorizationBuilder()
               .SetDefaultPolicy(new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                                                 .RequireAuthenticatedUser()
                                                                 .Build());

        builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        return builder;
    }
}
