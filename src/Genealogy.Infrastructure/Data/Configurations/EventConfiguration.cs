using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.ToTable("events")
               .HasKey(x => new { x.Id });

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Name)
               .HasColumnName("name");

        builder.Property(x => x.Type)
               .HasColumnName("type");

        builder.Property(x => x.Location)
               .HasColumnName("location");

        builder.Property(x => x.Date)
               .HasColumnName("date");

        builder.Property(x => x.EndDate)
               .HasColumnName("endDate");

        builder.Property(x => x.Notes)
               .HasColumnName("notes");

        builder.HasMany(x => x.Sources)
               .WithMany(x => x.Events)
               .UsingEntity<EventSources>(
            builder => builder.HasOne(x => x.Sources).WithMany().HasForeignKey(x => x.SourceId),
            builder => builder.HasOne(x => x.Events).WithMany().HasForeignKey(x => x.EventId),
            ConfigureEntity);
    }

    private void ConfigureEntity(EntityTypeBuilder<EventSources> builder)
    {
        builder.ToTable("event_sources")
               .HasKey(x => new { x.EventId, x.SourceId });

        builder.Property(x => x.EventId)
               .HasColumnName("eventId");

        builder.Property(x => x.SourceId)
               .HasColumnName("sourceId");
    }

    private class EventSources
    {
        public required Guid EventId { get; init; }

        public required EventEntity Events { get; init; }

        public required Guid SourceId { get; init; }

        public required SourceEntity Sources { get; init; }
    }
}