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
            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Job_OrganizationId_JobId",
                table: "Highlight");

            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Project_ProjectId_JobId",
                table: "Highlight");

            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Job_OrganizationId_JobId",
                table: "Highlight",
                columns: new[] { "OrganizationId", "JobId" },
                principalTable: "Job",
                principalColumns: new[] { "OrganizationId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Project_ProjectId_JobId",
                table: "Highlight",
                columns: new[] { "ProjectId", "JobId" },
                principalTable: "Project",
                principalColumns: new[] { "Id", "JobId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Job_OrganizationId_JobId",
                table: "Highlight");

            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Project_ProjectId_JobId",
                table: "Highlight");

            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Job_OrganizationId_JobId",
                table: "Highlight",
                columns: new[] { "OrganizationId", "JobId" },
                principalTable: "Job",
                principalColumns: new[] { "OrganizationId", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Project_ProjectId_JobId",
                table: "Highlight",
                columns: new[] { "ProjectId", "JobId" },
                principalTable: "Project",
                principalColumns: new[] { "Id", "JobId" });
        }
    }
}
