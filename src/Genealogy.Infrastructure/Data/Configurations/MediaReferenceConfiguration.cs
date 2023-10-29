using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;
internal class MediaReferenceConfiguration : IEntityTypeConfiguration<MediaReference>
{
    public void Configure(EntityTypeBuilder<MediaReference> builder)
    {
        builder.ToTable("media_reference")
               .HasKey(x => new { x.EntityId, x.MediaId });

        builder.Property(x => x.MediaId)
               .HasColumnName("mediaId");

        builder.Property(x => x.EntityId)
               .HasColumnName("entityId");
    }
}
