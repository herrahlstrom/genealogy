namespace Genealogy.Web.Components.Person;

public class TimelineFilterItem
{
    public required EventType Type { get; init; }
    public required string DisplayName { get; init; }
    public bool Enabled { get; set; }
}
