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
                table: "Project",
                keyColumns: new[] { "Id", "JobId", "OrganizationId" },
                keyValues: new object[] { 5, 2, 1 },
                column: "Description",
                value: "Associated Foods is a cooperative network that provides food distribution, warehousing, and retail support to independent grocery stores.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId", "OrganizationId" },
                keyValues: new object[] { 5, 2, 1 },
                column: "Description",
                value: null);
        }
    }
}
