using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Repositories;

internal class EventRepository : IEventRepository
{
    private readonly GenealogyDbContext _dbContext;
    public EventRepository(GenealogyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(EventEntity entity)
    {
        _dbContext.Set<EventEntity>()
                  .Add(entity);
    }

    public async Task<IReadOnlyCollection<EventEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Set<EventEntity>()
                               .Include(x => x.Sources)
                               .ToListAsync(ct);
    }

    public async Task<EventEntity> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Set<EventEntity>()
                               .Where(x => x.Id == id)
                               .Include(x => x.Sources)
                               .FirstAsync(ct);
    }

    public void Remove(EventEntity entity)
    {
        _dbContext.Set<EventEntity>()
                  .Remove(entity);
    }
}
