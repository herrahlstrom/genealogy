using DbUp.Engine;
using Genealogy.Domain.Data;
using Genealogy.Infrastructure.Data;
using Genealogy.Infrastructure.Data.Migration;
using Genealogy.Infrastructure.Data.Migration.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Genealogy.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetConnectionString("Sqlite") is { } sqlite)
        {
            services.AddSingleton<IScriptProvider, SqliteScriptProvider>();
            services.AddDbContextFactory<GenealogyDbContext>(options => options.UseSqlite(sqlite));
        }
        else
        {
            services.AddDbContextFactory<GenealogyDbContext>(options => options.UseInMemoryDatabase("Genealogy"));
        }
        services.AddSingleton<IDbContextFactory, GenealogyDbContextFactory>();

        return services;
    }
}
