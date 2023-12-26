using Genealogy.Domain.Entities;

namespace Genealogy.Domain.Repositories;

public interface IPersonRepository : IEntityRepository<Guid, PersonEntity>
{
    public Task LoadEvents(PersonEntity entity);
    public Task<IReadOnlyCollection<PersonEntity>> FindByName(string name, CancellationToken cancellationToken);
}
