using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FamilyMemberNavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members");

            migrationBuilder.CreateIndex(
                name: "IX_family_members_personId",
                table: "family_members",
                column: "personId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members",
                columns: new[] { "familyId", "personId" },
                unique: true,
                filter: "memberType IN (1,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_family_members_families_familyId",
                table: "family_members",
                column: "familyId",
                principalTable: "families",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_family_members_persons_personId",
                table: "family_members",
                column: "personId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_family_members_families_familyId",
                table: "family_members");

            migrationBuilder.DropForeignKey(
                name: "FK_family_members_persons_personId",
                table: "family_members");

            migrationBuilder.DropIndex(
                name: "IX_family_members_personId",
                table: "family_members");

            migrationBuilder.DropIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members",
                columns: new[] { "familyId", "memberType" },
                unique: true,
                filter: "memberType IN (1,2)");
        }
    }
}
