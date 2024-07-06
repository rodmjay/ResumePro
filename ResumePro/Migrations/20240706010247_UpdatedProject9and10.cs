using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProject9and10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId", "OrganizationId" },
                keyValues: new object[] { 9, 8, 1 },
                column: "Description",
                value: "Cathexis is a platform that hosts extensive affiliate marketing campaigns, tracking clicks and conversions to inform business decisions.");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId", "OrganizationId" },
                keyValues: new object[] { 10, 8, 1 },
                column: "Description",
                value: "Monaco Classroom is a virtual learning environment that offers access to digital training materials purchased online.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId", "OrganizationId" },
                keyValues: new object[] { 9, 8, 1 },
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId", "OrganizationId" },
                keyValues: new object[] { 10, 8, 1 },
                column: "Description",
                value: null);
        }
    }
}
