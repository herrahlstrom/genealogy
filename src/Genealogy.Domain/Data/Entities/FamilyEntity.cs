namespace Genealogy.Domain.Data.Entities;

public class FamilyEntity : IEntity<Guid>
{
    public required Guid Id { get; init; }
    public string Notes { get; set; } = "";
}
