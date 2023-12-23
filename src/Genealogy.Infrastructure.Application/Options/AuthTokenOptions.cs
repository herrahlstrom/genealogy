namespace Genealogy.Infrastructure.Application.Options;

public class AuthOptions
{
    public const string SectionName = "Auth";
}

public class AuthTokenOptions
{
    public const string SectionName = $"{AuthOptions.SectionName}:Token";
    public required Uri Issuer { get; set; }
    public required TimeSpan AccessTokenLifetime { get; set; }
    public required TimeSpan IdentityTokenLifetime { get; set; }
}
