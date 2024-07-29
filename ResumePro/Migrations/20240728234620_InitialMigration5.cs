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
            migrationBuilder.DropForeignKey(
                name: "FK_PersonaLanguage_Persona_OrganizationId_PersonaId",
                table: "PersonaLanguage");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaLanguage_Persona_OrganizationId_PersonaId",
                table: "PersonaLanguage",
                columns: new[] { "OrganizationId", "PersonaId" },
                principalTable: "Persona",
                principalColumns: new[] { "OrganizationId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonaLanguage_Persona_OrganizationId_PersonaId",
                table: "PersonaLanguage");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaLanguage_Persona_OrganizationId_PersonaId",
                table: "PersonaLanguage",
                columns: new[] { "OrganizationId", "PersonaId" },
                principalTable: "Persona",
                principalColumns: new[] { "OrganizationId", "Id" });
        }
    }
}
