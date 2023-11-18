using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Genealogy.Infrastructure.Data;

public class GenealogyDbContextFactory : IDesignTimeDbContextFactory<GenealogyDbContext>, IDisposable
{
    private readonly ServiceProvider m_services;

    public GenealogyDbContextFactory()
    {
        string connectionString = "Data Source=G:\\Min enhet\\Släktforskning\\Data\\mydb.db;";
        m_services = new ServiceCollection().AddDbContextFactory<GenealogyDbContext>(b => b.UseSqlite(connectionString))
                                            .BuildServiceProvider();
    }

    GenealogyDbContext IDesignTimeDbContextFactory<GenealogyDbContext>.CreateDbContext(string[] args)
    {
        return m_services.GetRequiredService<GenealogyDbContext>();
    }

    public void Dispose()
    {
        m_services.Dispose();
    }
}