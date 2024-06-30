using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
                values: new object[,]
                {
                    { 1, 7, 1 },
                    { 1, 9, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 9,
                column: "Title",
                value: "Visual Studio 2022");

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 18, "Geneva" },
                    { 19, "Data Explorer" },
                    { 20, "Synapse" },
                    { 21, "Function Apps" },
                    { 22, "AppInsights" },
                    { 23, "Kusto" },
                    { 24, "Bicep" }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[,]
                {
                    { 1, 18, 8 },
                    { 1, 19, 8 },
                    { 1, 20, 8 },
                    { 1, 21, 8 },
                    { 1, 22, 8 },
                    { 1, 23, 8 },
                    { 1, 24, 8 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId", "ShowInSummary" },
                values: new object[,]
                {
                    { 1, 1, 18, false },
                    { 1, 1, 19, false },
                    { 1, 1, 20, false },
                    { 1, 1, 21, false },
                    { 1, 1, 22, false },
                    { 1, 1, 23, false },
                    { 1, 1, 24, false }
                });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
                values: new object[,]
                {
                    { 1, 18, 1 },
                    { 1, 19, 1 },
                    { 1, 20, 1 },
                    { 1, 21, 1 },
                    { 1, 22, 1 },
                    { 1, 23, 1 },
                    { 1, 24, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 21 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 23 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 1, 24 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 18 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 19 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 20 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 21 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 22 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 23 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 24 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 21 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 22 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 23 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 24 });

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 9,
                column: "Title",
                value: "Visual Studio 20229");
        }
    }
}
