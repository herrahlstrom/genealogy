using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class PersonRepository : EntityRepository<Guid, PersonEntity>, IPersonRepository
{
    public PersonRepository(DbSet<PersonEntity> dbSet) : base(dbSet)
    {

    }
}
