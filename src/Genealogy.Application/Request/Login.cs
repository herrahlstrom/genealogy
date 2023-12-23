using Genealogy.Api.Auth;
using MediatR;

namespace Genealogy.Application.Request;

public record LoginRequest(string Username, string Password) : IRequest<TokenResponse>;
