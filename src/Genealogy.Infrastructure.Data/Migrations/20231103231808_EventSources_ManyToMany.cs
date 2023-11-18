using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EventSources_ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_event_sources_sourceId",
                table: "event_sources",
                column: "sourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_event_sources_events_eventId",
                table: "event_sources",
                column: "eventId",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_sources_sources_sourceId",
                table: "event_sources",
                column: "sourceId",
                principalTable: "sources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_sources_events_eventId",
                table: "event_sources");

            migrationBuilder.DropForeignKey(
                name: "FK_event_sources_sources_sourceId",
                table: "event_sources");

            migrationBuilder.DropIndex(
                name: "IX_event_sources_sourceId",
                table: "event_sources");
        }
    }
}
