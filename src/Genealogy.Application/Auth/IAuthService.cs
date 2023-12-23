using System.Security.Claims;

namespace Genealogy.Application.Auth;

public interface IAuthService
{
    public Task<IEnumerable<Claim>> GetAuthClaims(Guid userId);
    public Task<User> Login(string username, string password);
}
