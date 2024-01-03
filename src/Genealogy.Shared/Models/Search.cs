using Genealogy.Shared;

namespace Genealogy.Web.Components.Search.Models;

public class SearchRequest()
{
    public required string Query { get; init; }
}

public class SearchResult()
{
    public required IList<SearchResultItem> Items { get; init; }
}

public class SearchResultItem
{
    public required DateModel BirthDate { get; init; }
    public required Guid Id { get; init; }
    public required PersonName Name { get; init; }
    public required string? Url { get; init; }
}
