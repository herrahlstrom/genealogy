using System;
using System.Linq;

namespace Genealogy.Shared.Models;

public class TimelineItem
{
    public DateModel? Date { get; init; }
    public required Guid EventId { get; init; }
    public ICollection<Tuple<string, Uri>>? Links { get; set; }
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
        DateTime a;
        DateTime b;

        if (birthDate is { Date: { } bDate })
        {
            a = bDate.ToDateTime(TimeOnly.MinValue);
        }
        else if (birthDate is { } bs && bs.Year is { } bYear)
        {
            a = new DateTime(bYear, 1, 1);
        }
        else
        {
            return null;
        }

        if (birthDate is { Date: { } refDate })
        {
            b = refDate.ToDateTime(TimeOnly.MinValue);
        }
        else if (birthDate is { } bs && bs.Year is { } refYear)
        {
            b = new DateTime(refYear, 1, 1);
        }
        else
        {
            return null;
        }

        if (b.DayOfYear >= a.DayOfYear)
        {
            return b.Year - a.Year;
        }
        else
        {
            return b.Year - a.Year - 1;
        }
    }
}
