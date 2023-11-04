﻿using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data.Repositories;

internal class PersonRepository : EntityRepository<PersonEntity>, IPersonRepository
{
    public PersonRepository(DbSet<PersonEntity> dbSet) : base(dbSet)
    {

    }

    public async Task<PersonEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(x => x.Id == id).FirstAsync(cancellationToken);
    }
}