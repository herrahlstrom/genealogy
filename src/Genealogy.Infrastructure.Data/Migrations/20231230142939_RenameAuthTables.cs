using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAuthTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_members_roles_role_id",
                schema: "auth",
                table: "role_members");

            migrationBuilder.DropForeignKey(
                name: "FK_role_members_users_user_id",
                schema: "auth",
                table: "role_members");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_permissions_permission_id",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_roles_role_id",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "auth",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rsa_keys",
                schema: "auth",
                table: "rsa_keys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                schema: "auth",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_members",
                schema: "auth",
                table: "role_members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refresh_token_data",
                schema: "auth",
                table: "refresh_token_data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permissions",
                schema: "auth",
                table: "permissions");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "auth",
                newName: "auth_users");

            migrationBuilder.RenameTable(
                name: "rsa_keys",
                schema: "auth",
                newName: "auth_rsa_keys");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "auth",
                newName: "auth_roles");

            migrationBuilder.RenameTable(
                name: "role_permissions",
                schema: "auth",
                newName: "auth_role_permissions");

            migrationBuilder.RenameTable(
                name: "role_members",
                schema: "auth",
                newName: "auth_role_members");

            migrationBuilder.RenameTable(
                name: "refresh_token_data",
                schema: "auth",
                newName: "auth_refresh_token_data");

            migrationBuilder.RenameTable(
                name: "permissions",
                schema: "auth",
                newName: "auth_permissions");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_role_id",
                table: "auth_role_permissions",
                newName: "IX_auth_role_permissions_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_members_user_id",
                table: "auth_role_members",
                newName: "IX_auth_role_members_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_users",
                table: "auth_users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_rsa_keys",
                table: "auth_rsa_keys",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_roles",
                table: "auth_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_role_permissions",
                table: "auth_role_permissions",
                columns: new[] { "permission_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_role_members",
                table: "auth_role_members",
                columns: new[] { "role_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_refresh_token_data",
                table: "auth_refresh_token_data",
                column: "token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_permissions",
                table: "auth_permissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_auth_role_members_auth_roles_role_id",
                table: "auth_role_members",
                column: "role_id",
                principalTable: "auth_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_auth_role_members_auth_users_user_id",
                table: "auth_role_members",
                column: "user_id",
                principalTable: "auth_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_auth_role_permissions_auth_permissions_permission_id",
                table: "auth_role_permissions",
                column: "permission_id",
                principalTable: "auth_permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_auth_role_permissions_auth_roles_role_id",
                table: "auth_role_permissions",
                column: "role_id",
                principalTable: "auth_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auth_role_members_auth_roles_role_id",
                table: "auth_role_members");

            migrationBuilder.DropForeignKey(
                name: "FK_auth_role_members_auth_users_user_id",
                table: "auth_role_members");

            migrationBuilder.DropForeignKey(
                name: "FK_auth_role_permissions_auth_permissions_permission_id",
                table: "auth_role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_auth_role_permissions_auth_roles_role_id",
                table: "auth_role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_users",
                table: "auth_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_rsa_keys",
                table: "auth_rsa_keys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_roles",
                table: "auth_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_role_permissions",
                table: "auth_role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_role_members",
                table: "auth_role_members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_refresh_token_data",
                table: "auth_refresh_token_data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_permissions",
                table: "auth_permissions");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.RenameTable(
                name: "auth_users",
                newName: "users",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth_rsa_keys",
                newName: "rsa_keys",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth_roles",
                newName: "roles",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth_role_permissions",
                newName: "role_permissions",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth_role_members",
                newName: "role_members",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth_refresh_token_data",
                newName: "refresh_token_data",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth_permissions",
                newName: "permissions",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_auth_role_permissions_role_id",
                schema: "auth",
                table: "role_permissions",
                newName: "IX_role_permissions_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_auth_role_members_user_id",
                schema: "auth",
                table: "role_members",
                newName: "IX_role_members_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "auth",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rsa_keys",
                schema: "auth",
                table: "rsa_keys",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                schema: "auth",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                schema: "auth",
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_members",
                schema: "auth",
                table: "role_members",
                columns: new[] { "role_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_refresh_token_data",
                schema: "auth",
                table: "refresh_token_data",
                column: "token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permissions",
                schema: "auth",
                table: "permissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_members_roles_role_id",
                schema: "auth",
                table: "role_members",
                column: "role_id",
                principalSchema: "auth",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_members_users_user_id",
                schema: "auth",
                table: "role_members",
                column: "user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_permissions_permission_id",
                schema: "auth",
                table: "role_permissions",
                column: "permission_id",
                principalSchema: "auth",
                principalTable: "permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_roles_role_id",
                schema: "auth",
                table: "role_permissions",
                column: "role_id",
                principalSchema: "auth",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
