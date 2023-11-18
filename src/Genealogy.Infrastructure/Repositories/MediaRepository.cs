using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Repositories;

internal class MediaRepository : EntityRepository<Guid, MediaEntity>, IMediaRepository
{
    public MediaRepository(DbSet<MediaEntity> dbSet) : base(dbSet)
    {

    }
}
