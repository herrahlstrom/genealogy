using System;
using System.Linq;

namespace Genealogy.Web.Models.Person;

public class PersonTree
{
    public ParentPersonTreeNode? Father { get; set; }
    public ParentPersonTreeNode? Mother { get; set; }
    public required TreePerson Self { get; set; }
    public ICollection<PersonTreeFamily>? Families { get; set; }
}

public record TreePerson(Guid Id, PersonName Name, PersonSex Sex, DateModel BirthDate, DateModel DeathDate);
public record PersonTreeFamily(TreePerson? Partner, ICollection<TreePerson> Children, ICollection<TreePerson>? FosterChildren = null);
public record ParentPersonTreeNode(Guid Id, PersonName Name, PersonSex Sex, ParentPersonTreeNode? Father, ParentPersonTreeNode? Mother, DateModel BirthDate, DateModel DeathDate)
    : TreePerson(Id, Name, Sex, BirthDate, DeathDate);
