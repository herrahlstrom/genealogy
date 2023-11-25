using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitEventMembers3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_members_family_EventMember_eventId_entityId",
                table: "event_members_family");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_person_EventMember_eventId_entityId",
                table: "event_members_person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventMember",
                table: "EventMember");

            migrationBuilder.RenameTable(
                name: "EventMember",
                newName: "event_members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_members",
                table: "event_members",
                columns: new[] { "eventId", "entityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_family_event_members_eventId_entityId",
                table: "event_members_family",
                columns: new[] { "eventId", "entityId" },
                principalTable: "event_members",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_person_event_members_eventId_entityId",
                table: "event_members_person",
                columns: new[] { "eventId", "entityId" },
                principalTable: "event_members",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_members_family_event_members_eventId_entityId",
                table: "event_members_family");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_person_event_members_eventId_entityId",
                table: "event_members_person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_members",
                table: "event_members");

            migrationBuilder.RenameTable(
                name: "event_members",
                newName: "EventMember");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventMember",
                table: "EventMember",
                columns: new[] { "eventId", "entityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_family_EventMember_eventId_entityId",
                table: "event_members_family",
                columns: new[] { "eventId", "entityId" },
                principalTable: "EventMember",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_person_EventMember_eventId_entityId",
                table: "event_members_person",
                columns: new[] { "eventId", "entityId" },
                principalTable: "EventMember",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
