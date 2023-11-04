﻿using Genealogy.Domain.Data.Entities;

namespace Genealogy.Domain.Data.Repositories;

public interface IMediaRepository : IEntityRepository<MediaEntity>
{
    Task<MediaEntity> GetById(Guid id, CancellationToken cancellationToken = default);
}