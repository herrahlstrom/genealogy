using Genealogy.Domain.Data.Entities;

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
    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
}

public interface IPersonRepository
{
    PersonEntity Add();
    Task<PersonEntity> GetAsync(Guid id);
}
