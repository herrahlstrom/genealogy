using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface ISourceRepository : IEntityRepository<Guid, SourceEntity>
{
}