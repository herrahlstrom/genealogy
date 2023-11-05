using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Genealogy.Infrastructure.Data.Repositories;

internal abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public EntityRepository(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }

    protected DbSet<TEntity> DbSet => _dbSet;

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellation)
    {
        return await DbSet.ToListAsync(cancellation);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}

internal abstract class EntityRepository<TKey, TEntity> : EntityRepository<TEntity>, IEntityRepository<TKey, TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
{
    public EntityRepository(DbSet<TEntity> dbSet) : base(dbSet)
    {
    }

    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(x => x.Id.Equals(id)).FirstAsync(cancellationToken);        
    }
}