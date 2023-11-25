using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Genealogy.Domain.Data.Entities;

public abstract class EventMember()
{
    public string? Date { get; set; }
    public string? EndDate { get; set; }
    public required Guid EntityId { get; init; }
    public required Guid EventId { get; init; }
    public required EventType EventType { get; init; }

    public EventEntity Event { get; private set; } = default!;
}

public class PersonEventMember() : EventMember;
public class FamilyEventMember() : EventMember;
public class UnknownEventMember() : EventMember;
