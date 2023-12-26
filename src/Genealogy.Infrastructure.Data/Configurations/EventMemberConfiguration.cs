using System;
using System.Linq;
using Genealogy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class PersonEventMemberConfiguration : IEntityTypeConfiguration<PersonEventMember>
{
    public void Configure(EntityTypeBuilder<PersonEventMember> builder)
    {
        builder.ToTable("event_members_person");
    }
}

internal class FamilyEventMemberConfiguration : IEntityTypeConfiguration<FamilyEventMember>
{
    public void Configure(EntityTypeBuilder<FamilyEventMember> builder)
    {
        builder.ToTable("event_members_family");
    }
}

internal class EventMemberConfiguration : IEntityTypeConfiguration<EventMember>
{
    public void Configure(EntityTypeBuilder<EventMember> builder)
    {
        builder.ToTable("event_members")
               .UseTptMappingStrategy()
               .HasKey(x => new { x.EventId, x.EntityId });

        builder.Property(x => x.EntityId)
               .HasColumnName("entityId");

        builder.Property(x => x.EventId)
               .HasColumnName("eventId");

        builder.Property(x => x.EventType)
               .HasColumnName("type");

        builder.Property(x => x.Date)
               .HasColumnName("date");

        builder.Property(x => x.EndDate)
               .HasColumnName("endDate");

        builder.HasOne(x => x.Event).WithMany().HasForeignKey(x => x.EventId);
    }
}
