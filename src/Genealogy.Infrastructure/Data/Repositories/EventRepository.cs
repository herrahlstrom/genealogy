using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class EventRepository : EntityRepository<EventEntity>, IEventRepository
{
    public EventRepository(DbSet<EventEntity> dbSet) : base(dbSet)
    {

    }

    public async Task<EventEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(x => x.Id == id).FirstAsync(cancellationToken);
    }
}
