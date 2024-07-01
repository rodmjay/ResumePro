using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Persona",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GitHub", "LinkedIn" },
                values: new object[] { "https://www.github.com/rodmjay", "https://www.linkedin.com/in/rodmjay" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Persona",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GitHub", "LinkedIn" },
                values: new object[] { "https://www.linkedin.com/in/rodmjay", "https://www.github.com/rodmjay" });
        }
    }
}
