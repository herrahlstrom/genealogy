namespace Genealogy.Domain.Data;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateDbContext();
}

