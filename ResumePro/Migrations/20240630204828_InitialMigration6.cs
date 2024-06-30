using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 1, 1, 3 },
                    { 1, 1, 4 },
                    { 1, 1, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 2 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 4 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 5 });
        }
    }
}
