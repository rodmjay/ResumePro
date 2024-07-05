using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSpanish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Code3",
                keyValue: "spa",
                columns: new[] { "Name", "NativeName" },
                values: new object[] { "Spanish", "español" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Code3",
                keyValue: "spa",
                columns: new[] { "Name", "NativeName" },
                values: new object[] { "Spanish; Castilian", "español, castellano" });
        }
    }
}
