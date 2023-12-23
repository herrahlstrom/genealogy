using System.Security.Claims;
using Genealogy.Api.Auth;
using Genealogy.Application.Auth;
using Microsoft.Extensions.Logging;

namespace Genealogy.Infrastructure.Application.Auth;

internal class DummyAuthService(ILogger<DummyAuthService> logger) : IAuthService
{
    private static readonly Guid MartinUserId = new("4fe0610c-81b7-49b4-aca8-93e746ff8389");

    public async Task<IEnumerable<Claim>> GetAuthClaims(Guid userId)
    {
        await Task.Delay(1);

        if (userId == MartinUserId)
        {
            return [
                new Claim(GenealogyClaimTypes.Permission, PermissionValues.Read),
                new Claim(GenealogyClaimTypes.Permission, PermissionValues.Write),
                new Claim(GenealogyClaimTypes.Permission, PermissionValues.Admin)];
        }
        else
        {
            return [];
        }
    }

    public async Task<User> Login(string username, string password)
    {
        await Task.Delay(1);

        if (username.Equals("martin", StringComparison.OrdinalIgnoreCase))
        {
            var user = new User(MartinUserId, "Martin Ahlström");
            logger.LogInformation("User {User} logged in by faked credentials", user.Name);
            return user;
        }

        throw new UnauthorizedAccessException("Invalid username or/and password.");
    }
}

