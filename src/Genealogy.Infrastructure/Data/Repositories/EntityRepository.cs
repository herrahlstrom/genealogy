using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet;
    public EntityRepository(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellation)
    {
        return await _dbSet.ToListAsync(cancellation);
    }
}
