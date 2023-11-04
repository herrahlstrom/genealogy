using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface ISourceRepository : IEntityRepository<SourceEntity>
{
    Task<SourceEntity> GetById(Guid id, CancellationToken cancellationToken = default);
}