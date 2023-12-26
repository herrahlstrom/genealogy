namespace Genealogy.Domain.Exceptions;

public class InvalidTokenException(string message) : Exception(message)
{
}
