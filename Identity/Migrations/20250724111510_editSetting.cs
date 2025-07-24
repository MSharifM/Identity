using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    /// <inheritdoc />
    public partial class editSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LatTimeChange",
                table: "SiteSettings",
                newName: "LastTimeChanged");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastTimeChanged",
                table: "SiteSettings",
                newName: "LatTimeChange");
        }
    }
}
