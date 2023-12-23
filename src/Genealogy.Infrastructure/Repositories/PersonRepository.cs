using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Repositories;

internal class PersonRepository : EntityRepository<Guid, PersonEntity>, IPersonRepository
{
    private readonly GenealogyDbContext m_dbContext;

    public PersonRepository(GenealogyDbContext dbContext) : base(dbContext.Set<PersonEntity>())
    {
        m_dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PersonEntity>> FindByName(string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        IQueryable<PersonEntity> q = m_dbContext.Persons;
        foreach (var str in name.ToLowerInvariant().Split())
        {
            q = q.Where(x => x.Name.Contains(str));
        }
        return await q.ToListAsync(cancellationToken);
    }

    public async Task LoadEvents(PersonEntity entity)
    {
        await m_dbContext.Entry(entity)
                          .Collection(x => x.Events)
                          .LoadAsync();
        foreach (var e in entity.Events)
        {
            await m_dbContext.Entry(e)
                             .Reference(x => x.Event)
                             .LoadAsync();
        }
    }
}
