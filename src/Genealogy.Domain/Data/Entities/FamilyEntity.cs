namespace Genealogy.Domain.Data.Entities;

public class FamilyEntity
{
    public required Guid Id { get; init; }
    public string Notes { get; set; } = "";
}
