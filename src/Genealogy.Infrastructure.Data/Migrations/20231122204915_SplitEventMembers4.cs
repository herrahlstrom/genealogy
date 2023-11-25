using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitEventMembers4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "persons",
                type: "TEXT",
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_events_eventId",
                table: "event_members",
                column: "eventId",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_family_events_eventId",
                table: "event_members_family",
                column: "eventId",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_members_person_events_eventId",
                table: "event_members_person",
                column: "eventId",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_members_events_eventId",
                table: "event_members");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_family_events_eventId",
                table: "event_members_family");

            migrationBuilder.DropForeignKey(
                name: "FK_event_members_person_events_eventId",
                table: "event_members_person");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "persons",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldCollation: "NOCASE");
        }
    }
}
