using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;
internal class EventSourcesConfiguration : IEntityTypeConfiguration<EventSources>
{
    public void Configure(EntityTypeBuilder<EventSources> builder)
    {
        builder.ToTable("event_sources")
               .HasKey(x => new { x.EventId, x.SourceId });

        builder.Property(x => x.EventId)
               .HasColumnName("eventId");

        builder.Property(x => x.SourceId)
               .HasColumnName("sourceId");
    }
}
