using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class SourceRepository : EntityRepository<SourceEntity>, ISourceRepository
{
    public SourceRepository(DbSet<SourceEntity> dbSet) : base(dbSet)
    {

    }

    public async Task<SourceEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(x => x.Id == id).FirstAsync(cancellationToken);
    }
}
