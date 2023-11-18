using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Genealogy.Domain.Data.Entities;

public class EventMember
{
    public string? Date { get; set; }
    public string? EndDate { get; set; }
    public required Guid EntityId { get; init; }
    public required Guid EventId { get; init; }
    public required EventType Type { get; set; }
}
