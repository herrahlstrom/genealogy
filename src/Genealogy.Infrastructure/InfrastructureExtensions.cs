using Genealogy.Domain;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Genealogy.Infrastructure;

public static class InfrastructureExtensions
{
    public static THostBuilder AddInfrastructure<THostBuilder>(this THostBuilder builder) where THostBuilder : IHostApplicationBuilder
    {
        builder.Services.AddInfrastructure(builder.Configuration);

        return builder;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetConnectionString("Sqlite") is { } sqlite)
        {
            services.AddDbContextFactory<GenealogyDbContext>(options => options.UseSqlite(sqlite));
        }
        else
        {
            throw new InvalidOperationException("No connection string was configured");
        }
                
        services.AddSingleton<IAuthService, AuthService>();

        return services;
    }
}
