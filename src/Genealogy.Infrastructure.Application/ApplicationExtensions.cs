using System;
using System.Linq;
using Genealogy.Application;
using Genealogy.Application.Auth;
using Genealogy.Infrastructure.Application.Auth;
using Genealogy.Infrastructure.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Genealogy.Infrastructure.Application;

public static class ApplicationExtensions
{
    public static THostBuilder AddApplication<THostBuilder>(this THostBuilder builder)
        where THostBuilder : IHostApplicationBuilder
    {
        builder.Services.AddApplication(builder.Configuration);

        return builder;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));

        services.AddSingleton<ITimeService, TimeService>()
                .AddSingleton<IKeyStore, KeyStore>()
                .AddSingleton<IAuthService, DummyAuthService>()
                .AddSingleton<ITokenService, TokenService>();


        services
               .AddOptions<AuthOptions>()
               .Bind(configuration.GetSection(AuthOptions.SectionName));

        services
               .AddOptions<AuthTokenOptions>()
               .Bind(configuration.GetSection(AuthTokenOptions.SectionName));

        return services;
    }
}
