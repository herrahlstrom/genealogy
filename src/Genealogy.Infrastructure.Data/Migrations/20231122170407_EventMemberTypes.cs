using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EventMemberTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discriminator",
                table: "event_members",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discriminator",
                table: "event_members");
        }
    }
}
