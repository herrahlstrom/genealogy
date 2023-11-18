using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class SourceConfiguration : IEntityTypeConfiguration<SourceEntity>
{
    public void Configure(EntityTypeBuilder<SourceEntity> builder)
    {
        builder.ToTable("sources")
               .HasKey(x => new { x.Id });

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Name)
               .HasColumnName("name");

        builder.Property(x => x.Notes)
               .HasColumnName("notes");

        builder.Property(x => x.Page)
               .HasColumnName("page");

        builder.Property(x => x.ReferenceId)
               .HasColumnName("referenceId");

        builder.Property(x => x.Repository)
               .HasColumnName("repository");

        builder.Property(x => x.Type)
               .HasColumnName("type");

        builder.Property(x => x.Url)
               .HasColumnName("url");

        builder.Property(x => x.Volume)
               .HasColumnName("volume");

        builder.HasMany(x => x.Media)
               .WithMany()
               .UsingEntity("SourceMedia",
               l => l.HasOne(typeof(MediaEntity)).WithMany().HasForeignKey("MediaId"),
               r => r.HasOne(typeof(SourceEntity)).WithMany().HasForeignKey("SourceId"),
               j =>
               {
                   j.ToTable("source_media");
                   j.Property<Guid>("SourceId").HasColumnName("sourceId");
                   j.Property<Guid>("MediaId").HasColumnName("mediaId");
                   j.HasKey("SourceId", "MediaId");
               });
    }
}
