namespace Genealogy.Domain.Entities;

public class PersonEntity : IEntity<Guid>
{
    private List<FamilyMember> _families = new List<FamilyMember>();

    public IReadOnlyCollection<FamilyMember> Families
    {
        get => _families;
        private set => _families = new List<FamilyMember>(value);
    }

    public required Guid Id { get; init; }

    public ICollection<MediaEntity> Media { get; } = new List<MediaEntity>();

    public required string Name { get; set; }

    public string Notes { get; set; } = "";

    public string Profession { get; set; } = "";

    public required string Sex { get; set; }

    public ICollection<PersonEventMember> Events { get; } = new List<PersonEventMember>(0);
}
