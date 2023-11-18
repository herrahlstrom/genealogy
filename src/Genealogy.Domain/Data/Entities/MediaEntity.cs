namespace Genealogy.Domain.Data.Entities;

public class MediaEntity : IEntity<Guid>
{
    public required Guid Id { get; init; }
    public required MediaType Type { get; set; }
    public required string Path { get; set; }
    public long? Size { get; set; }
    public string? Title { get; set; }
    public string? FileCrc { get; set; }
    public string? Notes { get; set; }
    public ICollection<MediaMeta> Meta { get; set; } = default!;
}

public class MediaMeta
{
    public required string Key { get; init; }
    public required string Value { get; set; }
}
