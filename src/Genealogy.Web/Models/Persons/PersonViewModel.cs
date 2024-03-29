﻿using Genealogy.Web.Models.Media;

namespace Genealogy.Web.Models.Person;

public class PersonViewModel
{
    public required Guid Id { get; init; }
    public required PersonName Name { get; init; }
    public string? Notes { get; init; }
    public required PersonSex Sex { get; init; }
    public string? Profession { get; init; }
    public PersonTree? Tree { get; init; }

    public IReadOnlyList<TimelineItem>? TimelineItems { get; init; }
    public required IReadOnlyCollection<MediaViewModel> Media { get; set; }
}
