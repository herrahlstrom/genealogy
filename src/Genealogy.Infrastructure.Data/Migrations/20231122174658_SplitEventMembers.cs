using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitEventMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discriminator",
                table: "event_members");

            migrationBuilder.CreateTable(
                name: "FamilyEventMember",
                columns: table => new
                {
                    entityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    eventId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyEventMember", x => new { x.eventId, x.entityId });
                    table.ForeignKey(
                        name: "FK_FamilyEventMember_event_members_eventId_entityId",
                        columns: x => new { x.eventId, x.entityId },
                        principalTable: "event_members",
                        principalColumns: new[] { "eventId", "entityId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyEventMember_families_entityId",
                        column: x => x.entityId,
                        principalTable: "families",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonEventMember",
                columns: table => new
                {
                    entityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    eventId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEventMember", x => new { x.eventId, x.entityId });
                    table.ForeignKey(
                        name: "FK_PersonEventMember_event_members_eventId_entityId",
                        columns: x => new { x.eventId, x.entityId },
                        principalTable: "event_members",
                        principalColumns: new[] { "eventId", "entityId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonEventMember_persons_entityId",
                        column: x => x.entityId,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyEventMember_entityId",
                table: "FamilyEventMember",
                column: "entityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEventMember_entityId",
                table: "PersonEventMember",
                column: "entityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyEventMember");

            migrationBuilder.DropTable(
                name: "PersonEventMember");

            migrationBuilder.AddColumn<int>(
                name: "discriminator",
                table: "event_members",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
