using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 10,
                column: "ProjectId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 11,
                column: "ProjectId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 10,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 11,
                column: "ProjectId",
                value: null);
        }
    }
}
