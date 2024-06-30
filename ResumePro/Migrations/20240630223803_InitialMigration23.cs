using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 17, 5, 1, null, "Managed 4 developers directly using Agile Scrum" },
                    { 18, 5, 1, null, "Architected complex, mission critical, authentication and authorization features for banking platform including: Fingerprint Login, Friends and Family Shared Banking" }
                });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
                values: new object[,]
                {
                    { 5, 1, 1 },
                    { 5, 3, 1 },
                    { 5, 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 25, "Api Integrations" },
                    { 26, "Orchard" },
                    { 27, "Objective-C" },
                    { 28, "Bluetooth Low Energy (BLE)" }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[,]
                {
                    { 1, 25, 8 },
                    { 1, 26, 8 },
                    { 1, 27, 8 },
                    { 1, 28, 8 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId", "ShowInSummary" },
                values: new object[,]
                {
                    { 1, 1, 25, false },
                    { 1, 1, 26, false },
                    { 1, 1, 27, false },
                    { 1, 1, 28, false }
                });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
                values: new object[,]
                {
                    { 5, 25, 1 },
                    { 5, 26, 1 },
                    { 5, 27, 1 },
                    { 5, 28, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 25 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 26 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 27 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 5, 28 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 25 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 26 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 27 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 28 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 25 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 26 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 27 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 28 });

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 28);
        }
    }
}
