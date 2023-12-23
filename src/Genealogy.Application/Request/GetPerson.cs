using MediatR;

namespace Genealogy.Application.Request;

public record GetPersonRequest(Guid Id) : IRequest<GetPersonResponse>;

public record GetPersonResponse(Guid Id, string Name, string BirthDate);
