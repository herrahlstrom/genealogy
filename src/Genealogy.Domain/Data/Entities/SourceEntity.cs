namespace Genealogy.Domain.Data.Entities;

public class SourceEntity
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public string Notes { get; set; } = "";
    public string? Page { get; set; }
    public string? ReferenceId { get; set; }
    public string? Repository { get; set; }
    public SourceType Type { get; set; }
    public string? Url { get; set; }
    public string? Volume { get; set; }
    public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
}
