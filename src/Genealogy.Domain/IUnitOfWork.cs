using Genealogy.Domain.Repositories;

namespace Genealogy.Domain;

public interface IUnitOfWork : IDisposable
{
    IEventRepository EventRepository { get; }
    IFamilyRepository FamilyRepository { get; }
    IMediaRepository MediaRepository { get; }
    IPersonRepository PersonRepository { get; }
    ISourceRepository SourceRepository { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}

