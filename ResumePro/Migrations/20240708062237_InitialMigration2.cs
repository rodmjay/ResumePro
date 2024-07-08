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
            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId", "Rating" },
                values: new object[] { 1, 1, 96, 9 });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 6 },
                    { 1, 1, 1, 10 },
                    { 1, 1, 1, 39 }
                });

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "C#");

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                values: new object[] { 1, 1, 1, 96 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 6 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 10 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 39 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 96 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 96 });

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "C# Development");
        }
    }
}
