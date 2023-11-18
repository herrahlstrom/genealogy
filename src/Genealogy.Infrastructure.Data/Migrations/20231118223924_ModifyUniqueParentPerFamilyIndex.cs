using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUniqueParentPerFamilyIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members",
                columns: new[] { "familyId", "personId" },
                unique: true,
                filter: "memberType IN (1,2)");
        }
    }
}
