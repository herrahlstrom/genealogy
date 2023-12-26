namespace Genealogy.Domain.Entities;

public class FamilyEntity : IEntity<Guid>
{
    private List<FamilyMember> _familyMembers = new List<FamilyMember>();

    public IReadOnlyCollection<FamilyMember> FamilyMembers
    {
        get => _familyMembers;
        private set => _familyMembers = new List<FamilyMember>(value);
    }

    public required Guid Id { get; init; }

    public string Notes { get; set; } = "";

    public ICollection<FamilyEventMember> Events { get; } = new List<FamilyEventMember>(0);

}
