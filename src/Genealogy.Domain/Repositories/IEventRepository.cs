using Genealogy.Domain.Entities;

namespace Genealogy.Domain.Repositories;

public interface IEventRepository : IEntityRepository<Guid, EventEntity>
{
}
