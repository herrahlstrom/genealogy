namespace Genealogy.Domain.Entities.Auth;

public class Role
{
    public required string Id { get; init; }

    public List<User> Members { get; } = [];
}
