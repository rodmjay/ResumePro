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
            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Company_OrganizationId_PersonId_CompanyId",
                table: "Highlight");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Company_OrganizationId_PersonId_CompanyId",
                table: "Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Company_OrganizationId_PersonId_CompanyId",
                table: "Highlight",
                columns: new[] { "OrganizationId", "PersonId", "CompanyId" },
                principalTable: "Company",
                principalColumns: new[] { "OrganizationId", "PersonId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Company_OrganizationId_PersonId_CompanyId",
                table: "Project",
                columns: new[] { "OrganizationId", "PersonId", "CompanyId" },
                principalTable: "Company",
                principalColumns: new[] { "OrganizationId", "PersonId", "Id" });
        }
    }
}
