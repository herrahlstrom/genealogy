using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class FamilyRepository : EntityRepository<FamilyEntity>, IFamilyRepository
{
    public FamilyRepository(DbSet<FamilyEntity> dbSet) : base(dbSet)
    {

    }

    public async Task<FamilyEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(x => x.Id == id).FirstAsync(cancellationToken);
    }
}
