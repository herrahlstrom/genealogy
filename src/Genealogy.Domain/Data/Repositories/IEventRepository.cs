using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IEventRepository : IEntityRepository<Guid, EventEntity>
{
}
