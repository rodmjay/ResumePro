using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 15, 1, 5, 1, "Introduced enhanced unit and integration testing framework patterns" },
                    { 16, 1, 6, 1, "Maintaned 90% Code Coverage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 16);
        }
    }
}
