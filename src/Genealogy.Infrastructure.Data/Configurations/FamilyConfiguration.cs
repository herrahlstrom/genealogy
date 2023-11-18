﻿using System;
using System.Linq;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class FamilyConfiguration : IEntityTypeConfiguration<FamilyEntity>
{
    public void Configure(EntityTypeBuilder<FamilyEntity> builder)
    {
        builder.ToTable("families")
               .HasKey(x => new { x.Id });

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Notes)
               .HasColumnName("notes");
    }
}