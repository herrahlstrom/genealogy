using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class MediaMetaConfiguration : IEntityTypeConfiguration<MediaMeta>
{
    public void Configure(EntityTypeBuilder<MediaMeta> builder)
    {
        builder.ToTable("media_meta")
               .HasKey(x => new { x.MediaId, x.Key });

        builder.Property(x => x.MediaId)
               .HasColumnName("media_id");

        builder.Property(x => x.Key)
               .HasColumnName("key");

        builder.Property(x => x.Value)
               .HasColumnName("value");
    }
}
