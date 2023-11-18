using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

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
               .UsingEntity("EventSources",
               l => l.HasOne(typeof(SourceEntity)).WithMany().HasForeignKey("SourceId"),
               r => r.HasOne(typeof(EventEntity)).WithMany().HasForeignKey("EventId"),
               j =>
               {
                   j.ToTable("event_sources");
                   j.Property<Guid>("EventId").HasColumnName("eventId");
                   j.Property<Guid>("SourceId").HasColumnName("sourceId");
                   j.HasKey("EventId", "SourceId");
               });
    }
}