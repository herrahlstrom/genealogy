using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;
internal class MediaRepository : EntityRepository<MediaEntity>, IMediaRepository
{
    public MediaRepository(DbSet<MediaEntity> dbSet) : base(dbSet)
    {

    }
}
