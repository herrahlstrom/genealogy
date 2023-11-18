using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        /* Shadow properties */
        const string FamilyIdProperty = "FamilyId";
        const string PersonIdProperty = "PersonId";

        builder.ToTable("family_members")
               .HasKey(FamilyIdProperty, PersonIdProperty);

        builder.Property(FamilyIdProperty)
               .HasColumnName("familyId");

        builder.Property(PersonIdProperty)
               .HasColumnName("personId");

        builder.Property(x => x.MemberType)
               .HasColumnName("memberType")
               .HasConversion<int>();

        /* Indecies */

        builder
            .HasIndex(FamilyIdProperty, nameof(FamilyMember.MemberType))
            .HasFilter($"memberType IN ({(int)FamilyMemberType.Husband},{(int)FamilyMemberType.Wife})")
            .IsUnique()
            .HasDatabaseName("IX_UniqueParentPerFamily");

        /* Discriminator */

        builder.HasDiscriminator<FamilyMemberType>(x => x.MemberType)
               .HasValue<FamilyHusbandMember>(FamilyMemberType.Husband)
               .HasValue<FamilyWifeMember>(FamilyMemberType.Wife)
               .HasValue<FamilyChildMember>(FamilyMemberType.Child)
               .HasValue<FamilyFosterChildMember>(FamilyMemberType.FosterChild);

        /* Navigations */
        
        builder.HasOne(x=> x.Family).WithMany(x=> x.FamilyMembers).HasForeignKey(FamilyIdProperty).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x=> x.Person).WithMany(x=> x.Families).HasForeignKey(PersonIdProperty).OnDelete(DeleteBehavior.Restrict);
    }
}
