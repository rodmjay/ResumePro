using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResumeReference",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeReference", x => new { x.OrganizationId, x.ReferenceId, x.ResumeId });
                    table.ForeignKey(
                        name: "FK_ResumeReference_Reference_OrganizationId_PersonaId_ReferenceId",
                        columns: x => new { x.OrganizationId, x.PersonaId, x.ReferenceId },
                        principalTable: "Reference",
                        principalColumns: new[] { "OrganizationId", "PersonaId", "Id" });
                    table.ForeignKey(
                        name: "FK_ResumeReference_Resume_OrganizationId_ResumeId",
                        columns: x => new { x.OrganizationId, x.ResumeId },
                        principalTable: "Resume",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.InsertData(
                table: "ResumeReference",
                columns: new[] { "OrganizationId", "ReferenceId", "ResumeId", "PersonaId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 2, 1, 1 },
                    { 1, 5, 1, 1 },
                    { 1, 6, 1, 1 },
                    { 1, 7, 1, 1 },
                    { 1, 8, 1, 1 },
                    { 1, 9, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeReference_OrganizationId_PersonaId_ReferenceId",
                table: "ResumeReference",
                columns: new[] { "OrganizationId", "PersonaId", "ReferenceId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeReference_OrganizationId_ResumeId",
                table: "ResumeReference",
                columns: new[] { "OrganizationId", "ResumeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResumeReference");
        }
    }
}
