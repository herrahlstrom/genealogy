using Genealogy.Domain.Data.Repositories;

namespace Genealogy.Domain.Data;

public interface IUnitOfWork : IDisposable
{
    IEventRepository EventRepository { get; }
    IFamilyRepository FamilyRepository { get; }
    IMediaRepository MediaRepository { get; }
    IPersonRepository PersonRepository { get; }
    ISourceRepository SourceRepository { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}

