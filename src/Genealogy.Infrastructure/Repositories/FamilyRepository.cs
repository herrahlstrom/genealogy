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

    public Task LoadEvents(FamilyEntity entity)
    {
        return m_dbContext.Entry(entity).Collection(x=> x.Events).LoadAsync();
    }
}
