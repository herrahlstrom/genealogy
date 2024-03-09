using Genealogy.Web.Models.Media;

namespace Genealogy.Web.Models.Sources;

public class SourceViewModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Repository { get; init; }
    public required string Volume { get; init; }
    public required string Page { get; init; }
    public required string Url { get; init; }
    public required string ReferenceId { get; init; }
    public required string Notes { get; init; }
    public required IReadOnlyCollection<MediaViewModel> Media { get; set; }
}
