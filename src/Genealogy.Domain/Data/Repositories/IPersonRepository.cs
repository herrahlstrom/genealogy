using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IPersonRepository : IEntityRepository<Guid, PersonEntity>
{
    public Task LoadEvents(PersonEntity entity);
    public Task<IReadOnlyCollection<PersonEntity>> FindByName(string name, CancellationToken cancellationToken);
}
