using DbUp;
using DbUp.Engine;
using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Entities;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data.Configurations;
using Genealogy.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

internal class GenealogyDbContext : DbContext, IDbContext
{
    private readonly Lazy<IEntityRepository<EventMember>> _eventMemberRepository;
    private readonly Lazy<IEntityRepository<EventEntity>> _eventRepository;
    private readonly Lazy<IEntityRepository<EventSources>> _eventSourcesRepository;
    private readonly Lazy<IEntityRepository<FamilyMember>> _familyMemberRepository;
    private readonly Lazy<IEntityRepository<FamilyEntity>> _familyRepository;
    private readonly Lazy<IEntityRepository<MediaReference>> _mediaReferencesRepository;
    private readonly Lazy<IEntityRepository<MediaEntity>> _mediaRepository;
    private readonly Lazy<IEntityRepository<PersonEntity>> _personRepository;
    private readonly IScriptProvider _scriptProvider;
    private readonly Lazy<IEntityRepository<SourceEntity>> _sourceRepository;

    public GenealogyDbContext(DbContextOptions<GenealogyDbContext> options, IScriptProvider scriptProvider) : base(options)
    {
        _scriptProvider = scriptProvider;
        _personRepository = new Lazy<IEntityRepository<PersonEntity>>(() => new EntityRepository<PersonEntity>(base.Set<PersonEntity>()));
        _eventRepository = new Lazy<IEntityRepository<EventEntity>>(() => new EntityRepository<EventEntity>(base.Set<EventEntity>()));
        _familyRepository = new Lazy<IEntityRepository<FamilyEntity>>(() => new EntityRepository<FamilyEntity>(base.Set<FamilyEntity>()));
        _mediaRepository = new Lazy<IEntityRepository<MediaEntity>>(() => new EntityRepository<MediaEntity>(base.Set<MediaEntity>()));
        _sourceRepository = new Lazy<IEntityRepository<SourceEntity>>(() => new EntityRepository<SourceEntity>(base.Set<SourceEntity>()));
        _familyMemberRepository = new Lazy<IEntityRepository<FamilyMember>>(() => new EntityRepository<FamilyMember>(base.Set<FamilyMember>()));
        _eventMemberRepository = new Lazy<IEntityRepository<EventMember>>(() => new EntityRepository<EventMember>(base.Set<EventMember>()));
        _eventSourcesRepository = new Lazy<IEntityRepository<EventSources>>(() => new EntityRepository<EventSources>(base.Set<EventSources>()));
        _mediaReferencesRepository = new Lazy<IEntityRepository<MediaReference>>(() => new EntityRepository<MediaReference>(base.Set<MediaReference>()));
    }

    public IEntityRepository<EventMember> EventMemberRepository => _eventMemberRepository.Value;

    public IEntityRepository<EventEntity> EventRepository => _eventRepository.Value;

    public IEntityRepository<EventSources> EventSourcesRepository => _eventSourcesRepository.Value;

    public IEntityRepository<FamilyMember> FamilyMemberRepository => _familyMemberRepository.Value;

    public IEntityRepository<FamilyEntity> FamilyRepository => _familyRepository.Value;
    public IEntityRepository<MediaReference> MediaReferencesRepository => _mediaReferencesRepository.Value;

    public IEntityRepository<MediaEntity> MediaRepository => _mediaRepository.Value;

    public IEntityRepository<PersonEntity> PersonRepository => _personRepository.Value;

    public IEntityRepository<SourceEntity> SourceRepository => _sourceRepository.Value;

    public void MigrateDatabase()
    {
        var upgrader = DeployChanges.To
                                   .SQLiteDatabase(Database.GetConnectionString())
                                   .WithScripts(_scriptProvider)
                                   .LogToConsole()
                                   .WithTransaction()
                                   .Build();

        if (upgrader.IsUpgradeRequired())
        {
            upgrader.PerformUpgrade();

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenealogyDbContext).Assembly);
    }
}
