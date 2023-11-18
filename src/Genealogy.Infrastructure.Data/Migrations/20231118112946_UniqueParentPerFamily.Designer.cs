﻿// <auto-generated />
using System;
using Genealogy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    [DbContext(typeof(GenealogyDbContext))]
    [Migration("20231118112946_UniqueParentPerFamily")]
    partial class UniqueParentPerFamily
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("EventSources", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT")
                        .HasColumnName("eventId");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("sourceId");

                    b.HasKey("EventId", "SourceId");

                    b.HasIndex("SourceId");

                    b.ToTable("event_sources", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.EventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<string>("EndDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("endDate");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.EventMember", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT")
                        .HasColumnName("eventId");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("TEXT")
                        .HasColumnName("entityId");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<string>("EndDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("endDate");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER")
                        .HasColumnName("type");

                    b.HasKey("EventId", "EntityId");

                    b.ToTable("event_members", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.FamilyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.HasKey("Id");

                    b.ToTable("families", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.FamilyMember", b =>
                {
                    b.Property<Guid>("FamilyId")
                        .HasColumnType("TEXT")
                        .HasColumnName("familyId");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("TEXT")
                        .HasColumnName("personId");

                    b.Property<int>("MemberType")
                        .HasColumnType("INTEGER")
                        .HasColumnName("memberType");

                    b.HasKey("FamilyId", "PersonId");

                    b.HasIndex("FamilyId", "MemberType")
                        .IsUnique()
                        .HasDatabaseName("IX_UniqueParentPerFamily")
                        .HasFilter("memberType IN (1,2)");

                    b.ToTable("family_members", (string)null);

                    b.HasDiscriminator<int>("MemberType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.MediaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("FileCrc")
                        .HasColumnType("TEXT")
                        .HasColumnName("fileCrc");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<long?>("Size")
                        .HasColumnType("INTEGER")
                        .HasColumnName("size");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("media", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("profession");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("sex");

                    b.HasKey("Id");

                    b.ToTable("persons", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.SourceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.Property<string>("Page")
                        .HasColumnType("TEXT")
                        .HasColumnName("page");

                    b.Property<string>("ReferenceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("referenceId");

                    b.Property<string>("Repository")
                        .HasColumnType("TEXT")
                        .HasColumnName("repository");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER")
                        .HasColumnName("type");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT")
                        .HasColumnName("url");

                    b.Property<string>("Volume")
                        .HasColumnType("TEXT")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.ToTable("sources", (string)null);
                });

            modelBuilder.Entity("PersonMedia", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("TEXT")
                        .HasColumnName("personId");

                    b.Property<Guid>("MediaId")
                        .HasColumnType("TEXT")
                        .HasColumnName("mediaId");

                    b.HasKey("PersonId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("person_media", (string)null);
                });

            modelBuilder.Entity("SourceMedia", b =>
                {
                    b.Property<Guid>("SourceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("sourceId");

                    b.Property<Guid>("MediaId")
                        .HasColumnType("TEXT")
                        .HasColumnName("mediaId");

                    b.HasKey("SourceId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("source_media", (string)null);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.FamilyChildMember", b =>
                {
                    b.HasBaseType("Genealogy.Domain.Data.Entities.FamilyMember");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.FamilyFosterChildMember", b =>
                {
                    b.HasBaseType("Genealogy.Domain.Data.Entities.FamilyMember");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.FamilyHusbandMember", b =>
                {
                    b.HasBaseType("Genealogy.Domain.Data.Entities.FamilyMember");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.FamilyWifeMember", b =>
                {
                    b.HasBaseType("Genealogy.Domain.Data.Entities.FamilyMember");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("EventSources", b =>
                {
                    b.HasOne("Genealogy.Domain.Data.Entities.EventEntity", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Genealogy.Domain.Data.Entities.SourceEntity", null)
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Genealogy.Domain.Data.Entities.MediaEntity", b =>
                {
                    b.OwnsMany("Genealogy.Domain.Data.Entities.MediaMeta", "Meta", b1 =>
                        {
                            b1.Property<Guid>("MediaId")
                                .HasColumnType("TEXT")
                                .HasColumnName("media_id");

                            b1.Property<string>("Key")
                                .HasColumnType("TEXT")
                                .HasColumnName("key");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("value");

                            b1.HasKey("MediaId", "Key");

                            b1.ToTable("media_meta", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MediaId");
                        });

                    b.Navigation("Meta");
                });

            modelBuilder.Entity("PersonMedia", b =>
                {
                    b.HasOne("Genealogy.Domain.Data.Entities.MediaEntity", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Genealogy.Domain.Data.Entities.PersonEntity", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SourceMedia", b =>
                {
                    b.HasOne("Genealogy.Domain.Data.Entities.MediaEntity", null)
                        .WithMany()
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Genealogy.Domain.Data.Entities.SourceEntity", null)
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
