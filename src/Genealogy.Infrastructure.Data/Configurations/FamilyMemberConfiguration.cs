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
        builder.ToTable("family_members")
               .HasKey(x => new { x.FamilyId, x.PersonId });

        builder.Property(x => x.FamilyId)
               .HasColumnName("familyId");

        builder.Property(x => x.PersonId)
               .HasColumnName("personId");

        builder.Property(x => x.MemberType)
               .HasColumnName("memberType");
    }
}
