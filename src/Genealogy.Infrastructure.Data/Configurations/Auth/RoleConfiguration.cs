using System;
using System.Linq;
using Genealogy.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations.Auth;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        /* Table */

        builder.ToTable("auth_roles")
               .HasKey(x => x.Id);

        /* Properties */

        builder.Property(x => x.Id)
               .HasColumnName("id");

        /* Navigation */

        builder.HasMany(x => x.Members)
               .WithMany(x=> x.Roles)
               .UsingEntity<RoleMember>(entityTypeBuilder =>
               {
                   entityTypeBuilder.ToTable("auth_role_members");
                   entityTypeBuilder.Property(x => x.RoleId).HasColumnName("role_id");
                   entityTypeBuilder.Property(x => x.UserId).HasColumnName("user_id");
               });
    }

    private class RoleMember
    {
        public required string RoleId { get; init; }

        public required Guid UserId { get; init; }
    }

    private class RolePermission
    {

        public required string PermissionId { get; init; }
        public required string RoleId { get; init; }
    }
}

