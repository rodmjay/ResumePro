using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "html");

            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "markdown");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "1_html");

            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "2_markdown");
        }
    }
}
