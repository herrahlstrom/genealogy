namespace Genealogy.Api.Auth;

public class TokenResponse
{
    public required string? AccessToken { get; init; }
    public required string? IdentityToken { get; init; }
}
