using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[] { 1, 1, "Test", "123-123-1234", "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
