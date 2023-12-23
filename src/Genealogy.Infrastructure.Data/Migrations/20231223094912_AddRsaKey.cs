using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRsaKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rsa_keys",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    activeFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    activeTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    pemKey = table.Column<string>(type: "TEXT", nullable: false),
                    validFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    validTo = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rsa_keys", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rsa_keys");
        }
    }
}
