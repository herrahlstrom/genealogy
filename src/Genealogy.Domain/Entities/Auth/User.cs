using Genealogy.Domain.Utilities;

namespace Genealogy.Domain.Entities.Auth;

public class User
{
    public Guid Id { get; init; }

    public required string Name { get; set; }

    public string? PasswordHash { get; private set; }

    public string? PasswordSalt { get; private set; }

    public List<Role> Roles { get; } = [];

    public required string Username { get; set; }

    public User SetPassword(string password)
    {
        ArgumentException.ThrowIfNullOrEmpty(password);

        PasswordSalt = Guid.NewGuid().ToString("N");
        PasswordHash = HashHelper.EncryptString(password, PasswordSalt);

        return this;
    }

    public bool VerifyPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || PasswordHash is null || PasswordSalt is null)
        {
            return false;
        }

        return HashHelper.EncryptString(password, PasswordSalt)
                         .Equals(PasswordHash, StringComparison.Ordinal);
    }
}
