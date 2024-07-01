using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobTitle",
                value: "Enterprise Application Architect");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobTitle",
                value: null);
        }
    }
}
