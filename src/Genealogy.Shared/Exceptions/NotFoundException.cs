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
public class SourceNotFoundException(string message) : NotFoundException(message)
{
    public static SourceNotFoundException Create(Guid id)
    {
        return new SourceNotFoundException($"No source with Id '{id}' found.");
    }
}
public class MediaNotFoundException(string message) : NotFoundException(message)
{
    public static MediaNotFoundException Create(Guid id)
    {
        return new MediaNotFoundException($"No media with Id '{id}' found.");
    }
}