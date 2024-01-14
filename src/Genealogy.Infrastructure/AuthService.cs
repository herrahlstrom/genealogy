using System.Security.Claims;
using Genealogy.Domain;
using Genealogy.Domain.Entities.Auth;
using Genealogy.Infrastructure.Data;
using Genealogy.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Genealogy.Infrastructure;

internal class AuthService(ILogger<AuthService> logger, IDbContextFactory<GenealogyDbContext> dbContextFactory) : IAuthService
{
    public async Task<User> CreateNewUser(string username, string password, string name)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Username = username
        };
        user.SetPassword(password);

        dbContext.Set<User>().Add(user);
        await dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<IReadOnlyCollection<Claim>> GetAuthClaims(Guid userId)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var roles = await (from user in dbContext.Auth.Users
                           where user.Id == userId
                           from role in user.Roles
                           select role.Id).Distinct()
                                          .ToListAsync();
        var result = new List<Claim>(roles.Count);
        result.AddRange(roles.Select(pId => new Claim(GenealogyClaimTypes.Role, pId)));
        return result;
    }

    public async Task<User> Login(string username, string password)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var user = await dbContext.Auth.Users
                                       .Where(x => x.Username == username)
                                       .FirstOrDefaultAsync() ?? throw new UserNotFoundException($"No user with username {username} was found.");

        if (!user.VerifyPassword(password))
        {
            throw new UnauthorizedAccessException("Invalid password.");
        }

        logger.LogInformation("User {User} logged in by credentials.", user.Name);
        return user;
    }
}