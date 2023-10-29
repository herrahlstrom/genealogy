using Genealogy.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

internal class GenealogyDbContextFactory : IDbContextFactory
{
    private readonly IDbContextFactory<GenealogyDbContext> _factory;

    public GenealogyDbContextFactory(IDbContextFactory<GenealogyDbContext> dbContextFactory)
    {
        _factory = dbContextFactory;
    }

    public IDbContext CreateDbContext()
    {
        return _factory.CreateDbContext();
    }
}