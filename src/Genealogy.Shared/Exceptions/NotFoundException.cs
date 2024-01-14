namespace Genealogy.Shared.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}

public class UserNotFoundException(string message) : NotFoundException(message)
{
}

public class PersonNotFoundException(string message) : NotFoundException(message)
{
    public static PersonNotFoundException Create(Guid id)
    {
        return new PersonNotFoundException($"No person with Id '{id}' found.");
    }
}