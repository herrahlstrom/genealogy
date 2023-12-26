using System;
using System.Linq;
using Genealogy.Domain.Entities;
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

        builder.OwnsMany(x => x.Meta, ConfigureOwnedNavigation);
    }

    private void ConfigureOwnedNavigation(OwnedNavigationBuilder<MediaEntity, MediaMeta> builder)
    {
        const string OwnerIdProperty = "MediaId";

        builder.WithOwner().HasForeignKey(OwnerIdProperty);

        builder.ToTable("media_meta");

        builder.Property<Guid>(OwnerIdProperty)
               .HasColumnName("media_id");

        builder.Property(x => x.Key)
               .HasColumnName("key");

        builder.Property(x => x.Value)
               .HasColumnName("value");

        builder.HasKey(OwnerIdProperty, nameof(MediaMeta.Key));
    }
}
