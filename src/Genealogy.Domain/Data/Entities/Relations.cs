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

public class FamilyMember
{
    public required Guid FamilyId { get; init; }
    public required FamilyMemberType MemberType { get; set; }
    public required Guid PersonId { get; init; }
}

public class MediaReference
{
    public required Guid MediaId { get; init; }
    public required Guid EntityId { get; init; }
}
