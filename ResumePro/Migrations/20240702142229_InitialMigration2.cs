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
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 2, 1 },
                column: "Description",
                value: "The SRE Team oversees the quality assurance and monitoring of Microsoft's internal systems.");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 3, 2 },
                column: "Description",
                value: "ElderKey is a platform that manages the health and wellness of senior citizens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 2, 1 },
                column: "Description",
                value: "SRE Team manages the overal qualtiy and monitorying of internal microsoft systems");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 3, 2 },
                column: "Description",
                value: "ElderCare is a platform that manages the health and wellness of senior citizens");
        }
    }
}
