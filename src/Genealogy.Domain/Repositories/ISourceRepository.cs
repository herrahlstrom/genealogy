using Genealogy.Domain.Entities;

namespace Genealogy.Domain.Repositories;

public interface ISourceRepository : IEntityRepository<Guid, SourceEntity>
{
}