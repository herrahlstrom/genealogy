using System;
using System.Linq;
using Genealogy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.ToTable("family_members")
               .HasKey(x => new { x.FamilyId, x.PersonId });

        builder.Property(x => x.FamilyId)
               .HasColumnName("familyId");

        builder.Property(x => x.PersonId)
               .HasColumnName("personId");

        builder.Property(x => x.MemberType)
               .HasColumnName("memberType")
               .HasConversion<int>();

        /* Indecies */

        builder
            .HasIndex(x => new { x.FamilyId, x.MemberType })
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

        builder.HasOne(x => x.Family)
               .WithMany(x => x.FamilyMembers)
               .HasForeignKey(x => x.FamilyId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Person)
               .WithMany(x => x.Families)
               .HasForeignKey(x => x.PersonId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
