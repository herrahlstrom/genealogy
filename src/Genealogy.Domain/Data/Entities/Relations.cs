using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Genealogy.Domain.Data.Entities;

public class EventMember
{

    public string? Date { get; set; }

    public string? EndDate { get; set; }
    public required Guid EntityId { get; set; }
    public required Guid LifeStoryId { get; set; }

    public required EventType Type { get; set; }
}

public class EventSources
{
    public required Guid EventId { get; set; }
    public required Guid SourceId { get; set; }
}

public class FamilyMember
{
    public required Guid FamilyId { get; set; }
    public required FamilyMemberType MemberType { get; set; }
    public required Guid PersonId { get; set; }
}

public class MediaReference
{
    public required Guid MediaId { get; set; }
    public required Guid EntityId { get; set; }
}
