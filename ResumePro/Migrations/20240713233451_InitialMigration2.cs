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
                name: "FK_Certification_Persona_OrganizationId_PersonaId",
                table: "Certification");

            migrationBuilder.AddForeignKey(
                name: "FK_Certification_Persona_OrganizationId_PersonaId",
                table: "Certification",
                columns: new[] { "OrganizationId", "PersonaId" },
                principalTable: "Persona",
                principalColumns: new[] { "OrganizationId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certification_Persona_OrganizationId_PersonaId",
                table: "Certification");

            migrationBuilder.AddForeignKey(
                name: "FK_Certification_Persona_OrganizationId_PersonaId",
                table: "Certification",
                columns: new[] { "OrganizationId", "PersonaId" },
                principalTable: "Persona",
                principalColumns: new[] { "OrganizationId", "Id" });
        }
    }
}
