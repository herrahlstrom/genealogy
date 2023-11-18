using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;

namespace Genealogy.Infrastructure.Repositories;

internal class MediaRepository : EntityRepository<Guid, MediaEntity>, IMediaRepository
{
    private readonly GenealogyDbContext m_dbContext;

    public MediaRepository(GenealogyDbContext dbContext) : base(dbContext.Set<MediaEntity>())
    {
        m_dbContext = dbContext;
    }
}
