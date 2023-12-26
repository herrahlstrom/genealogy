namespace Genealogy.Domain;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateUnitOfWork();
}

