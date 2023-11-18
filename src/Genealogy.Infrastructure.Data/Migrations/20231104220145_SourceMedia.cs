using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SourceMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "source_media",
                columns: table => new
                {
                    sourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    mediaId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_source_media", x => new { x.sourceId, x.mediaId });
                    table.ForeignKey(
                        name: "FK_source_media_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_source_media_sources_sourceId",
                        column: x => x.sourceId,
                        principalTable: "sources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_source_media_mediaId",
                table: "source_media",
                column: "mediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "source_media");
        }
    }
}
