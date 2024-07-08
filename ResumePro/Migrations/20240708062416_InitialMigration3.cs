using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 96 });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 7 },
                    { 1, 1, 1, 95 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 7 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 95 });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                values: new object[] { 1, 1, 1, 96 });
        }
    }
}
