using Genealogy.Domain.Data;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure;

internal class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<GenealogyDbContext> _factory;

    public UnitOfWorkFactory(IDbContextFactory<GenealogyDbContext> dbContextFactory)
    {
        _factory = dbContextFactory;
    }

    IUnitOfWork IUnitOfWorkFactory.CreateUnitOfWork()
    {
        var dbContext = _factory.CreateDbContext();
        return new UnitOfWork(dbContext);
    }
}
