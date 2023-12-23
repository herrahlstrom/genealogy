using System.Data;
using Genealogy.Api.Auth;
using Genealogy.Application.Auth;
using Genealogy.Application.Exceptions;
using Genealogy.Application.Request;
using Genealogy.Domain.Data.Entities;
using Genealogy.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Genealogy.Infrastructure.Application.Handlers;

public class GetPersonHandler(GenealogyDbContext dbContext) : IRequestHandler<GetPersonRequest, GetPersonResponse>
{
    public async Task<GetPersonResponse> Handle(GetPersonRequest request, CancellationToken cancellationToken)
    {
        var query = from p in dbContext.Persons
                    where p.Id == request.Id
                    select new
                    {
                        p.Id,
                        p.Name,
                        Birth = (from eventMember in p.Events
                                 where eventMember.Event.Type == EventType.Födelse
                                 select new
                                 {
                                     Date = eventMember.Date ?? eventMember.Event.Date
                                 }).FirstOrDefault()
                    };
        var entity = await query.FirstOrDefaultAsync(cancellationToken)
            ?? throw new PersonNotFoundException($"Person with id '{request.Id}' does not exists.");

        return new GetPersonResponse(entity.Id, entity.Name, entity.Birth.Date);
    }
}

public class LoginHandler(IAuthService authService, ITokenService tokenService, ILogger<LoginHandler> logger) : IRequestHandler<LoginRequest, TokenResponse>
{
    public async Task<TokenResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await authService.Login(request.Username, request.Password);
        var claims = await authService.GetAuthClaims(user.Id);

        return new TokenResponse
        {
            IdentityToken = await tokenService.CreateIdentityToken(user, cancellationToken),
            AccessToken = await tokenService.CreateAccessToken(claims, cancellationToken),
        };
    }
}