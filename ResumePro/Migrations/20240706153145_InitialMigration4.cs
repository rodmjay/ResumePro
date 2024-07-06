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
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobPosting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JobPosting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => new { x.OrganizationId, x.Id });
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPosting_OrganizationId_CompanyId",
                table: "JobPosting",
                columns: new[] { "OrganizationId", "CompanyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_Company_OrganizationId_CompanyId",
                table: "JobPosting",
                columns: new[] { "OrganizationId", "CompanyId" },
                principalTable: "Company",
                principalColumns: new[] { "OrganizationId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_Company_OrganizationId_CompanyId",
                table: "JobPosting");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_JobPosting_OrganizationId_CompanyId",
                table: "JobPosting");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobPosting");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "JobPosting");
        }
    }
}
