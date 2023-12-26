namespace Genealogy.Domain.Entities;

public class EventEntity : IEntity<Guid>
{
    public string? Date { get; set; }
    public string? EndDate { get; set; }
    public required Guid Id { get; init; }
    public string? Location { get; set; }
    public string? Name { get; set; }
    public string Notes { get; set; } = "";
    public ICollection<SourceEntity> Sources { get; set; } = new List<SourceEntity>();
    public required EventType Type { get; set; }
}
