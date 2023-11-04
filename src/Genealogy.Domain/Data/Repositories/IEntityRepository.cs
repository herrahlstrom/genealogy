namespace Genealogy.Domain.Data.Repositories;

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

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellation);
}
