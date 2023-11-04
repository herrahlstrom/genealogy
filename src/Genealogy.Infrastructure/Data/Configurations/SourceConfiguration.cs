﻿using System;
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
               b => b.HasOne(typeof(MediaEntity), "Media").WithMany().HasForeignKey("MediaId"),
               b => b.HasOne(typeof(SourceEntity), "Source").WithMany().HasForeignKey("SourceId"),
               b =>
               {
                   b.ToTable("source_media").HasKey("SourceId", "MediaId");
                   b.Property(typeof(Guid), "SourceId").HasColumnName("sourceId");
                   b.Property(typeof(Guid), "MediaId").HasColumnName("mediaId");
               });
    }
}
