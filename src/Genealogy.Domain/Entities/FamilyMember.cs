namespace Genealogy.Domain.Entities;

public class FamilyChildMember : FamilyMember
{
    public FamilyChildMember() : base(FamilyMemberType.Child) { }
}
public class FamilyFosterChildMember : FamilyMember
{
    public FamilyFosterChildMember() : base(FamilyMemberType.FosterChild) { }
}
public class FamilyParentMember : FamilyMember
{
    public FamilyParentMember() : base(FamilyMemberType.Parent) { }
}

public abstract class FamilyMember
{
    public FamilyMember(FamilyMemberType memberType)
    {
        MemberType = memberType;
    }
    
    public Guid PersonId { get; init; }
    public Guid FamilyId { get; init; }

    public required FamilyEntity Family { get; init; }
    public required PersonEntity Person { get; init; }
    public FamilyMemberType MemberType { get; }
}
