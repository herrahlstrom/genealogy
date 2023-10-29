using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;

namespace Genealogy.Domain.Data;

public interface IDbContextFactory
{
    IDbContext CreateDbContext();
}

public interface IDbContext : IDisposable
{
    IEntityRepository<PersonEntity> PersonRepository { get; }
    IEntityRepository<EventEntity> EventRepository { get; }
    IEntityRepository<FamilyEntity> FamilyRepository { get; }
    IEntityRepository<MediaEntity> MediaRepository { get; }
    IEntityRepository<SourceEntity> SourceRepository { get; }
    
    IEntityRepository<FamilyMember> FamilyMemberRepository { get; }
    IEntityRepository<EventMember> EventMemberRepository { get; }
    IEntityRepository<EventSources> EventSourcesRepository { get; }
    IEntityRepository<MediaReference> MediaReferencesRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public void MigrateDatabase();
}

