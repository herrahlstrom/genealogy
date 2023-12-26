using System;
using System.Linq;
using Genealogy.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations.Auth;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        /* Table */

        builder.ToTable("auth_users")
               .HasKey(x => x.Id);

        /* Properties */

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Name)
               .HasColumnName("name");

        builder.Property(x => x.Username)
               .HasColumnName("username");

        builder.Property(x => x.PasswordHash)
               .HasColumnName("password_hash");

        builder.Property(x => x.PasswordSalt)
               .HasColumnName("password_salt");
    }
}

