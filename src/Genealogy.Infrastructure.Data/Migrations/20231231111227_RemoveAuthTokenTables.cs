using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuthTokenTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_refresh_token_data");

            migrationBuilder.DropTable(
                name: "auth_role_permissions");

            migrationBuilder.DropTable(
                name: "auth_rsa_keys");

            migrationBuilder.DropTable(
                name: "auth_permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auth_permissions",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_refresh_token_data",
                columns: table => new
                {
                    token = table.Column<string>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    eol = table.Column<DateTime>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_refresh_token_data", x => x.token);
                });

            migrationBuilder.CreateTable(
                name: "auth_rsa_keys",
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
                    table.PrimaryKey("PK_auth_rsa_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_role_permissions",
                columns: table => new
                {
                    permission_id = table.Column<string>(type: "TEXT", nullable: false),
                    role_id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_role_permissions", x => new { x.permission_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_auth_role_permissions_auth_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "auth_permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_auth_role_permissions_auth_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "auth_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auth_role_permissions_role_id",
                table: "auth_role_permissions",
                column: "role_id");
        }
    }
}
