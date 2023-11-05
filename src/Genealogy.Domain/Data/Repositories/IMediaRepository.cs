using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IMediaRepository : IEntityRepository<Guid, MediaEntity>
{
}
