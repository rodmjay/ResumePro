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
            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 4, 2 },
                column: "Description",
                value: "Cashflow Tactics is a platform designed to enhance the profitability of real estate investors through a diverse array of strategic approaches.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 4, 2 },
                column: "Description",
                value: null);
        }
    }
}
