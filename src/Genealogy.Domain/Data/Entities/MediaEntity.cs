using System.Diagnostics;

namespace Genealogy.Domain.Data.Entities;


public class MediaEntity
{
    public required Guid Id { get; set; }
    public required MediaType Type { get; set; }
    public string Path { get; set; } = null!;
    public long Size { get; set; }
    public string? Title { get; set; }
    public string? FileCrc { get; set; }
    public string? Notes { get; set; }
}


[DebuggerDisplay("{Key} {Value}")]
public class MediaMeta
{
    public required Guid MediaId { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; }
}
