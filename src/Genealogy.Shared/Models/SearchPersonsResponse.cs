namespace Genealogy.Models;

public record SearchPersonsResponse(IReadOnlyList<SearchPersonsResponseItem> Items);

public record SearchPersonsResponseItem(Guid Id, string Name);
