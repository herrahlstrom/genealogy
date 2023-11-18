namespace Genealogy.Domain.Data.Entities;

public class FamilyChildMember : FamilyMember
{
    public FamilyChildMember() : base(FamilyMemberType.Child) { }
}
public class FamilyFosterChildMember : FamilyMember
{
    public FamilyFosterChildMember() : base(FamilyMemberType.FosterChild) { }
}
public class FamilyHusbandMember : FamilyMember
{
    public FamilyHusbandMember() : base(FamilyMemberType.Husband) { }
}

public abstract class FamilyMember
{
    public FamilyMember(FamilyMemberType memberType)
    {
        MemberType = memberType;
    }
    
    public required FamilyEntity Family { get; init; }
    public required PersonEntity Person { get; init; }
    public FamilyMemberType MemberType { get; }
}
public class FamilyWifeMember : FamilyMember
{
    public FamilyWifeMember() : base(FamilyMemberType.Wife) { }
}
