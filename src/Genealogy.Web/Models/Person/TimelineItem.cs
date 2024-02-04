using System;
using System.Linq;

namespace Genealogy.Web.Models.Person;

public class TimelineItem
{
    public DateModel? Date { get; init; }
    public required Guid EventId { get; init; }
    public ICollection<ILink>? Links { get; set; }
    public string? Location { get; init; }
    public required string Name { get; set; }
    public int? RelativeAge { get; set; }
    public required EventType Type { get; init; }

    public TimelineItem SetRelativeAge(DateModel? birthDate)
    {
        RelativeAge = GetRelativeAge(birthDate);
        return this;
    }

    private int? GetRelativeAge(DateModel? birthDate)
    {
        if (GetDateTime(birthDate) is { } a && GetDateTime(this.Date) is { } b)
            return b.DayOfYear >= a.DayOfYear
                ? b.Year - a.Year
                : b.Year - a.Year - 1;
        return null;
    }

    private static DateTime? GetDateTime(DateModel? dateModel)
    {
        if (dateModel is { Date: { } date })
            return date.ToDateTime(TimeOnly.MinValue);
        if (dateModel is { Year: { } year })
            return new DateTime(year, 1, 1);
        return null;
    }
}
