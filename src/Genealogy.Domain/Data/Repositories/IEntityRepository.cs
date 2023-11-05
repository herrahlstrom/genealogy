namespace Genealogy.Domain.Data.Repositories;

public interface IEntityRepository<TKey, TEntity> : IEntityRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
}

public interface IEntityRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Add(entity);
        }
    }

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellation = default);
    void Remove(TEntity entity);
}
