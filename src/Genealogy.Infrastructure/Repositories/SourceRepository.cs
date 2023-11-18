using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;

namespace Genealogy.Infrastructure.Repositories;

internal class SourceRepository : EntityRepository<Guid, SourceEntity>, ISourceRepository
{
    private readonly GenealogyDbContext m_dbContext;

    public SourceRepository(GenealogyDbContext dbContext) : base(dbContext.Set<SourceEntity>())
    {
        m_dbContext = dbContext;
    }
}
