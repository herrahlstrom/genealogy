using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

internal class UnitOfWork : IUnitOfWork
{
    private readonly GenealogyDbContext _dbContext;
    private IEntityRepository<EventMember>? _eventMemberRepository = null;
    private IEntityRepository<EventEntity>? _eventRepository = null;
    private IEntityRepository<EventSources>? _eventSourcesRepository = null;
    private IEntityRepository<FamilyMember>? _familyMemberRepository = null;
    private IEntityRepository<FamilyEntity>? _familyRepository = null;
    private IEntityRepository<MediaReference>? _mediaReferencesRepository = null;
    private IEntityRepository<MediaEntity>? _mediaRepository = null;
    private IEntityRepository<PersonEntity>? _personRepository = null;
    private IEntityRepository<SourceEntity>? _sourceRepository = null;

    public UnitOfWork(GenealogyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEntityRepository<EventMember> EventMemberRepository => _eventMemberRepository ??= new EntityRepository<EventMember>(_dbContext.Set<EventMember>());
    public IEntityRepository<EventEntity> EventRepository => _eventRepository ??= new EntityRepository<EventEntity>(_dbContext.Set<EventEntity>());
    public IEntityRepository<EventSources> EventSourcesRepository => _eventSourcesRepository ??= new EntityRepository<EventSources>(_dbContext.Set<EventSources>());
    public IEntityRepository<FamilyMember> FamilyMemberRepository => _familyMemberRepository ??= new EntityRepository<FamilyMember>(_dbContext.Set<FamilyMember>());
    public IEntityRepository<FamilyEntity> FamilyRepository => _familyRepository ??= new EntityRepository<FamilyEntity>(_dbContext.Set<FamilyEntity>());
    public IEntityRepository<MediaReference> MediaReferencesRepository => _mediaReferencesRepository ??= new EntityRepository<MediaReference>(_dbContext.Set<MediaReference>());
    public IEntityRepository<MediaEntity> MediaRepository => _mediaRepository ??= new EntityRepository<MediaEntity>(_dbContext.Set<MediaEntity>());
    public IEntityRepository<PersonEntity> PersonRepository => _personRepository ??= new EntityRepository<PersonEntity>(_dbContext.Set<PersonEntity>());
    public IEntityRepository<SourceEntity> SourceRepository => _sourceRepository ??= new EntityRepository<SourceEntity>(_dbContext.Set<SourceEntity>());

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
internal class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<GenealogyDbContext> _factory;

    public UnitOfWorkFactory(IDbContextFactory<GenealogyDbContext> dbContextFactory)
    {
        _factory = dbContextFactory;
    }

    IUnitOfWork IUnitOfWorkFactory.CreateDbContext()
    {
        var dbContext = _factory.CreateDbContext();
        return new UnitOfWork(dbContext);
    }
}
