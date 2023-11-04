using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IFamilyRepository : IEntityRepository<FamilyEntity>
{
    Task<FamilyEntity> GetById(Guid id, CancellationToken cancellationToken = default);
}
