﻿using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class EventMemberConfiguration : IEntityTypeConfiguration<EventMember>
{
    public void Configure(EntityTypeBuilder<EventMember> builder)
    {
        builder.ToTable("event_members")
               .HasKey(x => new { x.EventId, x.EntityId });

        builder.Property(x => x.EntityId)
               .HasColumnName("entityId");

        builder.Property(x => x.EventId)
               .HasColumnName("eventId");

        builder.Property(x => x.Type)
               .HasColumnName("type");

        builder.Property(x => x.Date)
               .HasColumnName("date");

        builder.Property(x => x.EndDate)
               .HasColumnName("endDate");
    }
}