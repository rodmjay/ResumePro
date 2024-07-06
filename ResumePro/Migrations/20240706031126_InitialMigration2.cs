using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HiringManager",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringManager", x => new { x.OrganizationId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPosting",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    HiringManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPosting", x => new { x.OrganizationId, x.Id });
                    table.ForeignKey(
                        name: "FK_JobPosting_HiringManager_OrganizationId_HiringManagerId",
                        columns: x => new { x.OrganizationId, x.HiringManagerId },
                        principalTable: "HiringManager",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_JobPosting_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    JobPostingId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_JobPosting_OrganizationId_JobPostingId",
                        columns: x => new { x.OrganizationId, x.JobPostingId },
                        principalTable: "JobPosting",
                        principalColumns: new[] { "OrganizationId", "Id" });
                    table.ForeignKey(
                        name: "FK_Application_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.InsertData(
                table: "JobCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Jobs related to developing and maintaining software applications.", "Software Development" },
                    { 2, "Positions focused on promoting products and services.", "Marketing" },
                    { 3, "Roles involved in selling products and services to customers.", "Sales" },
                    { 4, "Jobs related to recruiting", "Human Resources" },
                    { 5, "Positions focused on managing company finances and accounts.", "Finance" },
                    { 6, "Roles involved in supporting and assisting customers.", "Customer Service" },
                    { 7, "Jobs related to designing and building products or infrastructure.", "Engineering" },
                    { 8, "Positions focused on overseeing product development and strategy.", "Product Management" },
                    { 9, "Roles involved in managing day-to-day business operations.", "Operations" },
                    { 10, "Jobs related to legal compliance and providing legal advice.", "Legal" },
                    { 11, "Positions focused on administrative and clerical tasks.", "Administrative" },
                    { 12, "Jobs related to providing technical support and managing IT systems.", "IT Support" },
                    { 13, "Positions focused on innovating and developing new products.", "Research and Development" },
                    { 14, "Roles involved in teaching and educational administration.", "Education" },
                    { 15, "Jobs related to providing medical and healthcare services.", "Healthcare" },
                    { 16, "Positions focused on managing supply chain and distribution.", "Logistics" },
                    { 17, "Roles related to graphic design", "Creative" },
                    { 18, "Jobs focused on ensuring product or service quality.", "Quality Assurance" },
                    { 19, "Positions involved in the production of goods.", "Manufacturing" },
                    { 20, "Roles related to planning and overseeing projects.", "Project Management" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_OrganizationId_JobPostingId",
                table: "Application",
                columns: new[] { "OrganizationId", "JobPostingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Application_OrganizationId_PersonaId",
                table: "Application",
                columns: new[] { "OrganizationId", "PersonaId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobPosting_JobCategoryId",
                table: "JobPosting",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPosting_OrganizationId_HiringManagerId",
                table: "JobPosting",
                columns: new[] { "OrganizationId", "HiringManagerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "JobPosting");

            migrationBuilder.DropTable(
                name: "HiringManager");

            migrationBuilder.DropTable(
                name: "JobCategory");
        }
    }
}
