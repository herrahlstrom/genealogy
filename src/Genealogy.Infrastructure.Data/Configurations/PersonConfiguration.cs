using System;
using System.Diagnostics;
using System.Linq;
using Genealogy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genealogy.Infrastructure.Data.Configurations;

internal class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable("persons")
               .HasKey(x => new { x.Id });

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Name)
               .HasColumnName("name")
               .UseCollation("NOCASE");

        builder.Property(x => x.Notes)
               .HasColumnName("notes");

        builder.Property(x => x.Profession)
               .HasColumnName("profession");

        builder.Property(x => x.Sex)
               .HasColumnName("sex")
               .HasConversion<string>(sex => Convert(sex), str => Convert(str));


        builder.HasMany(x => x.Media)
               .WithMany()
               .UsingEntity("PersonMedia",
               b => b.HasOne(typeof(MediaEntity), "Media").WithMany().HasForeignKey("MediaId"),
               b => b.HasOne(typeof(PersonEntity), "Person").WithMany().HasForeignKey("PersonId"),
               b =>
               {
                   b.ToTable("person_media").HasKey("PersonId", "MediaId");
                   b.Property(typeof(Guid), "PersonId").HasColumnName("personId");
                   b.Property(typeof(Guid), "MediaId").HasColumnName("mediaId");
               });

        builder.HasMany(x => x.Events).WithOne().HasForeignKey(x => x.EntityId);
    }

    private static PersonSex Convert(string str) => str switch
    {
        "M" => PersonSex.Male,
        "F" => PersonSex.Female,
        _ => throw new ArgumentException($"Invalid sex; {str}")
    };

    private static string Convert(PersonSex sex) => sex switch
    {
        PersonSex.Male => "M",
        PersonSex.Female => "F",
        _ => throw new ArgumentException($"Invalid sex; {sex}")
    };
}
