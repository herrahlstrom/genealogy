using Genealogy.Domain.Data.Auth;
using Genealogy.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

public class GenealogyDbContext : DbContext
{
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<FamilyEntity> Families { get; set; }
    public DbSet<RsaKey> RsaKeys { get; set; }

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
