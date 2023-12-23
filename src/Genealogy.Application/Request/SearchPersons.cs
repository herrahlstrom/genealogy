using MediatR;

namespace Genealogy.Application.Request;

public record SearchPersonsRequest(string QueryString) : IRequest<SearchPersonsResponse>;

public record SearchPersonsResponse(IReadOnlyList<SearchPersonsResponseItem> Items);
public record SearchPersonsResponseItem(Guid Id, string Name);
