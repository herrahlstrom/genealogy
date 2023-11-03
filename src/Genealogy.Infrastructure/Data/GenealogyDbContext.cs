﻿using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

internal class GenealogyDbContext : DbContext
{

    public GenealogyDbContext(DbContextOptions<GenealogyDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenealogyDbContext).Assembly);
    }
}
