using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Repositories;
using Genealogy.Infrastructure.Data;
using Genealogy.Infrastructure.Repositories;

namespace Genealogy.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    private readonly GenealogyDbContext _dbContext;
    private EventRepository? _eventRepository = null;
    private FamilyRepository? _familyRepository = null;
    private MediaRepository? _mediaRepository = null;
    private PersonRepository? _personRepository = null;
    private SourceRepository? _sourceRepository = null;

    public UnitOfWork(GenealogyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEventRepository EventRepository => _eventRepository ??= new EventRepository(_dbContext);
    public IFamilyRepository FamilyRepository => _familyRepository ??= new FamilyRepository(_dbContext);
    public IMediaRepository MediaRepository => _mediaRepository ??= new MediaRepository(_dbContext);
    public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_dbContext);
    public ISourceRepository SourceRepository => _sourceRepository ??= new SourceRepository(_dbContext);

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
