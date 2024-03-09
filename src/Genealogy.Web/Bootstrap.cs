using Genealogy.Api.Auth;
using Genealogy.Web;
using Microsoft.AspNetCore.Authentication.Cookies;

public static class Bootstrap
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services
               .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.ExpireTimeSpan = TimeSpan.FromDays(7);
                   options.SlidingExpiration = true;
                   //options.AccessDeniedPath = "/Forbidden/";
                   options.LoginPath = "/auth/login";
               });

        services
               .AddAuthorizationBuilder()
               .AddPolicy(Policies.CanRead, builder =>
               {
                   builder.RequireAuthenticatedUser()
                          .RequireRole(Roles.Reader);
               });

        return services;
    }

    public static IServiceCollection AddGenealogyOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<MediaOptions>().Bind(configuration.GetSection(MediaOptions.SectionName));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheControl, CacheControl>();

        return services;
    }
}


