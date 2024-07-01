using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Resume",
                newName: "JobTitle");

            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobTitle",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "Resume",
                newName: "ShortName");

            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                column: "ShortName",
                value: "Enterprise Application Architect");
        }
    }
}
