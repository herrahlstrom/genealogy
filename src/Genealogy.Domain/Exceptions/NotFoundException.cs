namespace Genealogy.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}

public class UserNotFoundException(string message) : NotFoundException(message)
{
}
public class PersonNotFoundException(string message) : NotFoundException(message)
{
}