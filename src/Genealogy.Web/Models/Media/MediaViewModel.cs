namespace Genealogy.Web.Models.Media;

public class MediaViewModel
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Url { get; init; }
    public required string Notes { get; init; }
}