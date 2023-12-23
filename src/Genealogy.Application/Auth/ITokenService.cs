using System.Security.Claims;

namespace Genealogy.Application.Auth;

public interface ITokenService
{
    Task<string> CreateAccessToken(IEnumerable<Claim> claims, CancellationToken cancellationToken);
    Task<string> CreateIdentityToken(User user, CancellationToken cancellationToken);
}
