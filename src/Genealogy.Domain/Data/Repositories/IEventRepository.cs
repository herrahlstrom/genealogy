using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IEventRepository : IEntityRepository<EventEntity>
{
    Task<EventEntity> GetById(Guid id, CancellationToken cancellationToken = default);
}
