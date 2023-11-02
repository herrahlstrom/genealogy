using DbUp;
using DbUp.Engine;
using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data.Configurations;
using Genealogy.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

internal class GenealogyDbContext : DbContext
{

    public GenealogyDbContext(DbContextOptions<GenealogyDbContext> options) : base(options)
    {
    }


    public void MigrateDatabase()
    {
        //var upgrader = DeployChanges.To
        //                           .SQLiteDatabase(Database.GetConnectionString())
        //                           .WithScripts(_scriptProvider)
        //                           .LogToConsole()
        //                           .WithTransaction()
        //                           .Build();

        //if (upgrader.IsUpgradeRequired())
        //{
        //    upgrader.PerformUpgrade();

        //}
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenealogyDbContext).Assembly);

        Database.EnsureCreated();
    }
}
