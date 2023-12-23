using System;
using System.Linq;
using Genealogy.Domain.Data.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;
internal class RsaKeyConfiguration : IEntityTypeConfiguration<RsaKey>
{
    public void Configure(EntityTypeBuilder<RsaKey> builder)
    {
        /* Table */

        builder.ToTable("rsa_keys")
               .HasKey(x => x.Id);

        /* Properties */

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Created)
               .HasColumnName("created");

        builder.Property(x => x.ValidFrom)
               .HasColumnName("validFrom");

        builder.Property(x => x.ValidTo)
               .HasColumnName("validTo");

        builder.Property(x => x.ActiveFrom)
               .HasColumnName("activeFrom");

        builder.Property(x => x.ActiveTo)
               .HasColumnName("activeTo");

        builder.Property(x => x.PemKey)
               .HasColumnName("pemKey");
    }
}
