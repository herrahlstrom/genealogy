using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IPersonRepository : IEntityRepository<PersonEntity>
{
    Task<PersonEntity> GetById(Guid id, CancellationToken cancellationToken = default);
}
