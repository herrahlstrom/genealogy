using System;
using System.Linq;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genealogy.Infrastructure.Migration;

public class BloggingContextFactory : IDesignTimeDbContextFactory<GenealogyDbContext>, IDisposable
{
    private readonly ServiceProvider _services;

    public BloggingContextFactory()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                      .AddJsonFile("appsettings.json")
                                                      .Build();

        _services = new ServiceCollection().AddDbContext<GenealogyDbContext>(builder => builder.UseSqlite(configuration.GetConnectionString("Sqlite")))
                                           .BuildServiceProvider();
    }

    GenealogyDbContext IDesignTimeDbContextFactory<GenealogyDbContext>.CreateDbContext(string[] args)
    {
        return _services.GetRequiredService<GenealogyDbContext>();
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}