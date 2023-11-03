using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;

namespace Genealogy.Domain.Data;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateDbContext();
}

public interface IUnitOfWork : IDisposable
{
    IEntityRepository<PersonEntity> PersonRepository { get; }
    IEntityRepository<EventEntity> EventRepository { get; }
    IEntityRepository<FamilyEntity> FamilyRepository { get; }
    IEntityRepository<MediaEntity> MediaRepository { get; }
    IEntityRepository<SourceEntity> SourceRepository { get; }
    
    IEntityRepository<FamilyMember> FamilyMemberRepository { get; }
    IEntityRepository<EventMember> EventMemberRepository { get; }
    IEntityRepository<MediaReference> MediaReferencesRepository { get; }
    
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}

