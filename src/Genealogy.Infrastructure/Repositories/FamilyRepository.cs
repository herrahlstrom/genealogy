using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;

namespace Genealogy.Infrastructure.Repositories;

internal class FamilyRepository : EntityRepository<Guid, FamilyEntity>, IFamilyRepository
{
    private readonly GenealogyDbContext m_dbContext;

    public FamilyRepository(GenealogyDbContext dbContext) : base(dbContext.Set<FamilyEntity>())
    {
        m_dbContext = dbContext;
    }
}
