using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genealogy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MergeHusbandAndWifeIntoParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members");

            // old: [1] Husband, [2] Wife
            // new: [1] Parent
            migrationBuilder.UpdateData("family_members", "memberType", 2, "memberType", 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UniqueParentPerFamily",
                table: "family_members",
                columns: new[] { "familyId", "memberType" },
                unique: true,
                filter: "memberType IN (1,2)");
        }
    }
}
