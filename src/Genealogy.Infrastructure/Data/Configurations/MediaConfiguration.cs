using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class MediaConfiguration : IEntityTypeConfiguration<MediaEntity>
{
    public void Configure(EntityTypeBuilder<MediaEntity> builder)
    {
        builder.ToTable("media")
               .HasKey(x => new { x.Id });

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Type)
               .HasColumnName("type");

        builder.Property(x => x.Path)
               .HasColumnName("path");

        builder.Property(x => x.Size)
               .HasColumnName("size");

        builder.Property(x => x.Title)
               .HasColumnName("title");

        builder.Property(x => x.FileCrc)
               .HasColumnName("fileCrc");

        builder.Property(x => x.Notes)
               .HasColumnName("notes");
    }
}
