using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Repositories;

internal class FamilyRepository : EntityRepository<Guid, FamilyEntity>, IFamilyRepository
{
    public FamilyRepository(DbSet<FamilyEntity> dbSet) : base(dbSet)
    {

    }
}
