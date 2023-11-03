using Genealogy.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

internal class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<GenealogyDbContext> _factory;
    private bool migrated = false;

    public UnitOfWorkFactory(IDbContextFactory<GenealogyDbContext> dbContextFactory)
    {
        _factory = dbContextFactory;
    }

    IUnitOfWork IUnitOfWorkFactory.CreateDbContext()
    {
        lock(_factory)
        {
            var dbContext = _factory.CreateDbContext();
            if (!migrated)
            {
                dbContext.Database.Migrate();
                migrated = true;
            }
            return new UnitOfWork(dbContext);
        }
    }
}
