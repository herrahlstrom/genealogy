using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IFamilyRepository : IEntityRepository<Guid, FamilyEntity>
{
    public Task LoadEvents(FamilyEntity entity);
}
