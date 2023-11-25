using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitEventMembers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyEventMember_event_members_eventId_entityId",
                table: "FamilyEventMember");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyEventMember_families_entityId",
                table: "FamilyEventMember");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEventMember_event_members_eventId_entityId",
                table: "PersonEventMember");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEventMember_persons_entityId",
                table: "PersonEventMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEventMember",
                table: "PersonEventMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyEventMember",
                table: "FamilyEventMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_members",
                table: "event_members");

            migrationBuilder.RenameTable(
                name: "PersonEventMember",
                newName: "event_members_person");

            migrationBuilder.RenameTable(
                name: "FamilyEventMember",
                newName: "event_members_family");

            migrationBuilder.RenameTable(
                name: "event_members",
                newName: "EventMember");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEventMember_entityId",
                table: "event_members_person",
                newName: "IX_event_members_person_entityId");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyEventMember_entityId",
                table: "event_members_family",
                newName: "IX_event_members_family_entityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_members_person",
                table: "event_members_person",
                columns: new[] { "eventId", "entityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_members_family",
                table: "event_members_family",
                columns: new[] { "eventId", "entityId" });

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
                name: "FK_event_members_family_families_entityId",
                table: "event_members_family",
                column: "entityId",
                principalTable: "families",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_person_EventMember_eventId_entityId",
                table: "event_members_person",
                columns: new[] { "eventId", "entityId" },
                principalTable: "EventMember",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_person_persons_entityId",
                table: "event_members_person",
                column: "entityId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_members_family_EventMember_eventId_entityId",
                table: "event_members_family");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_family_families_entityId",
                table: "event_members_family");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_person_EventMember_eventId_entityId",
                table: "event_members_person");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_person_persons_entityId",
                table: "event_members_person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventMember",
                table: "EventMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_members_person",
                table: "event_members_person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_members_family",
                table: "event_members_family");

            migrationBuilder.RenameTable(
                name: "EventMember",
                newName: "event_members");

            migrationBuilder.RenameTable(
                name: "event_members_person",
                newName: "PersonEventMember");

            migrationBuilder.RenameTable(
                name: "event_members_family",
                newName: "FamilyEventMember");

            migrationBuilder.RenameIndex(
                name: "IX_event_members_person_entityId",
                table: "PersonEventMember",
                newName: "IX_PersonEventMember_entityId");

            migrationBuilder.RenameIndex(
                name: "IX_event_members_family_entityId",
                table: "FamilyEventMember",
                newName: "IX_FamilyEventMember_entityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_members",
                table: "event_members",
                columns: new[] { "eventId", "entityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEventMember",
                table: "PersonEventMember",
                columns: new[] { "eventId", "entityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyEventMember",
                table: "FamilyEventMember",
                columns: new[] { "eventId", "entityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyEventMember_event_members_eventId_entityId",
                table: "FamilyEventMember",
                columns: new[] { "eventId", "entityId" },
                principalTable: "event_members",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyEventMember_families_entityId",
                table: "FamilyEventMember",
                column: "entityId",
                principalTable: "families",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEventMember_event_members_eventId_entityId",
                table: "PersonEventMember",
                columns: new[] { "eventId", "entityId" },
                principalTable: "event_members",
                principalColumns: new[] { "eventId", "entityId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEventMember_persons_entityId",
                table: "PersonEventMember",
                column: "entityId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
