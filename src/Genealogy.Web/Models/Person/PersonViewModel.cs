namespace Genealogy.Web.Models.Person;

public class PersonViewModel
{
    public required Guid Id { get; init; }
    public required PersonName Name { get; init; }
    public required ICollection<PersonEventViewModel>? Events { get; init; }
}

public record PersonEventViewModel(Guid Id, string Name, DateModel? Date, DateModel? EndDate, string? Location);