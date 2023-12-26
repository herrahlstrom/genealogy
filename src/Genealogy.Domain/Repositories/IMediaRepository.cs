using Genealogy.Domain.Entities;

namespace Genealogy.Domain.Repositories;

public interface IMediaRepository : IEntityRepository<Guid, MediaEntity>
{
}
