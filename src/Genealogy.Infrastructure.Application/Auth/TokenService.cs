using System.Security.Claims;
using Genealogy.Api.Auth;
using Genealogy.Application;
using Genealogy.Application.Auth;
using Genealogy.Infrastructure.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Genealogy.Infrastructure.Application.Auth;

internal class TokenService(IOptions<AuthTokenOptions> options, ITimeService timeService, IKeyStore keyStore) : ITokenService
{
    public Task<string> CreateAccessToken(IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        return CreateToken(claims, options.Value.AccessTokenLifetime, cancellationToken);
    }

    public Task<string> CreateIdentityToken(User user, CancellationToken cancellationToken)
    {
        IEnumerable<Claim> claims = [
            new Claim(GenealogyClaimTypes.Identity.Sub, user.Id.ToString("D")),
            new Claim(GenealogyClaimTypes.Identity.Name, user.Name)];

        return CreateToken(claims, options.Value.IdentityTokenLifetime, cancellationToken);
    }

    private async Task<string> CreateToken(IEnumerable<Claim> claims, TimeSpan lifetime, CancellationToken cancellationToken)
    {
        var jwtHandler = new JsonWebTokenHandler();

        var key = await keyStore.GetActiveKey(cancellationToken);

        return jwtHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),

            Audience = options.Value.Issuer.AbsoluteUri,
            Issuer = options.Value.Issuer.AbsoluteUri,

            IssuedAt = timeService.UtcNow,
            Expires = timeService.UtcNow + lifetime,

            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
        });
    }
}
