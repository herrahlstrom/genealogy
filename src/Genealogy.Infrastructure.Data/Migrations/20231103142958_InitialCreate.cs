using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_members",
                columns: table => new
                {
                    entityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    eventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: true),
                    endDate = table.Column<string>(type: "TEXT", nullable: true),
                    type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_members", x => new { x.eventId, x.entityId });
                });

            migrationBuilder.CreateTable(
                name: "event_sources",
                columns: table => new
                {
                    eventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    sourceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_sources", x => new { x.eventId, x.sourceId });
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: true),
                    endDate = table.Column<string>(type: "TEXT", nullable: true),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    notes = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "families",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_families", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "family_members",
                columns: table => new
                {
                    familyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    personId = table.Column<Guid>(type: "TEXT", nullable: false),
                    memberType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_family_members", x => new { x.familyId, x.personId });
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<int>(type: "INTEGER", nullable: false),
                    path = table.Column<string>(type: "TEXT", nullable: false),
                    size = table.Column<long>(type: "INTEGER", nullable: true),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    fileCrc = table.Column<string>(type: "TEXT", nullable: true),
                    notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media_reference",
                columns: table => new
                {
                    mediaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    entityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_reference", x => new { x.entityId, x.mediaId });
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: false),
                    profession = table.Column<string>(type: "TEXT", nullable: false),
                    sex = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: false),
                    page = table.Column<string>(type: "TEXT", nullable: true),
                    referenceId = table.Column<string>(type: "TEXT", nullable: true),
                    repository = table.Column<string>(type: "TEXT", nullable: true),
                    type = table.Column<int>(type: "INTEGER", nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: true),
                    volume = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media_meta",
                columns: table => new
                {
                    media_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", nullable: false),
                    value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_meta", x => new { x.media_id, x.key });
                    table.ForeignKey(
                        name: "FK_media_meta_media_media_id",
                        column: x => x.media_id,
                        principalTable: "media",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_members");

            migrationBuilder.DropTable(
                name: "event_sources");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropTable(
                name: "family_members");

            migrationBuilder.DropTable(
                name: "media_meta");

            migrationBuilder.DropTable(
                name: "media_reference");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "sources");

            migrationBuilder.DropTable(
                name: "media");
        }
    }
}
