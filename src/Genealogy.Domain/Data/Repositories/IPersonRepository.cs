using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IPersonRepository : IEntityRepository<Guid, PersonEntity>
{
}
