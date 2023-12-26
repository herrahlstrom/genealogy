namespace Genealogy.Models;

public class TokenResponse
{
    public required string AccessToken { get; init; }
    public required long AccessTokenExpiresIn { get; init; }
    public required string IdentityToken { get; init; }
    public required long IdentityTokenExpiresIn { get; init; }
    public required string? RefreshToken { get; init; }
}
