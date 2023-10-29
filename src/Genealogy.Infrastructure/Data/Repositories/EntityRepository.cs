using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
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

    public ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
    {
        return _dbSet.FindAsync(keyValues);
    }
}
