using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;

namespace Genealogy.Infrastructure.Repositories;

internal class PersonRepository : EntityRepository<Guid, PersonEntity>, IPersonRepository
{
    private readonly GenealogyDbContext m_dbContext;

    public PersonRepository(GenealogyDbContext dbContext) : base(dbContext.Set<PersonEntity>())
    {
        m_dbContext = dbContext;
    }
}
