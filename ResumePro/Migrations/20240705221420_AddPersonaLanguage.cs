using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonaLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonaLanguage",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Code3 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proficiency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaLanguage", x => new { x.OrganizationId, x.PersonaId, x.Code3 });
                    table.ForeignKey(
                        name: "FK_PersonaLanguage_Language_Code3",
                        column: x => x.Code3,
                        principalTable: "Language",
                        principalColumn: "Code3",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaLanguage_Persona_OrganizationId_PersonaId",
                        columns: x => new { x.OrganizationId, x.PersonaId },
                        principalTable: "Persona",
                        principalColumns: new[] { "OrganizationId", "Id" });
                });

            migrationBuilder.InsertData(
                table: "PersonaLanguage",
                columns: new[] { "Code3", "OrganizationId", "PersonaId", "Proficiency" },
                values: new object[,]
                {
                    { "eng", 1, 1, 5 },
                    { "spa", 1, 1, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonaLanguage_Code3",
                table: "PersonaLanguage",
                column: "Code3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonaLanguage");
        }
    }
}
