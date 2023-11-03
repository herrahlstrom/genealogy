using System;
using System.Linq;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Genealogy.Infrastructure.Migration;

public class BloggingContextFactory : IDesignTimeDbContextFactory<GenealogyDbContext>
{
    IConfigurationRoot _configuration;

    public BloggingContextFactory()
    {
        _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                   .AddJsonFile("appsettings.json")
                                                   .Build();
    }

    GenealogyDbContext IDesignTimeDbContextFactory<GenealogyDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GenealogyDbContext>();
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));

        return new GenealogyDbContext(optionsBuilder.Options);
    }
}