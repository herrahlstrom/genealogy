namespace Genealogy.Domain.Data.Entities;

public class PersonEntity
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public string Notes { get; set; } = "";

    public string Profession { get; set; } = "";

    public required string Sex { get; set; }

    public ICollection<MediaEntity> Media { get; } = new List<MediaEntity>();
}
