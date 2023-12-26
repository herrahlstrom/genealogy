using Genealogy.Domain.Entities.Auth;
using System.Security.Claims;

namespace Genealogy.Domain;

public interface IAuthService
{
    public Task<IEnumerable<Claim>> GetAuthClaims(Guid userId);
    public Task<User> Login(string username, string password);
    public Task<User> CreateNewUser(string username, string password, string name);
}
