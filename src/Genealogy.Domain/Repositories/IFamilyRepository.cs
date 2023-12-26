using Genealogy.Domain.Entities;

namespace Genealogy.Domain.Repositories;

public interface IFamilyRepository : IEntityRepository<Guid, FamilyEntity>
{
    public Task LoadEvents(FamilyEntity entity);
}
