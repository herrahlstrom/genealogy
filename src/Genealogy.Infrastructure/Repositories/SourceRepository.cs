using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Repositories;

internal class SourceRepository : EntityRepository<Guid, SourceEntity>, ISourceRepository
{
    public SourceRepository(DbSet<SourceEntity> dbSet) : base(dbSet)
    {

    }
}
