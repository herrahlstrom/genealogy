using Genealogy.Domain.Entities;
using Genealogy.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Infrastructure.Data;

public class GenealogyDbContext : DbContext
{

    public GenealogyDbContext(DbContextOptions<GenealogyDbContext> options) : base(options)
    {
        Auth = new GenealogyAuthDbSets(this);
    }
    public GenealogyAuthDbSets Auth { get; }
    public DbSet<FamilyEntity> Families { get; set; }
    public DbSet<FamilyMember> FamilyMembers { get; set; }
    public DbSet<PersonEntity> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenealogyDbContext).Assembly);
    }

    public class GenealogyAuthDbSets(GenealogyDbContext dbContext)
    {
        public DbSet<Role> Roles => dbContext.Set<Role>();
        public DbSet<User> Users => dbContext.Set<User>();
    }
}
