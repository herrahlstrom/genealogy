using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class MediaRepository : EntityRepository<MediaEntity>, IMediaRepository
{
    public MediaRepository(DbSet<MediaEntity> dbSet) : base(dbSet)
    {

    }

    public async Task<MediaEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(x => x.Id == id).FirstAsync(cancellationToken);
    }
}
