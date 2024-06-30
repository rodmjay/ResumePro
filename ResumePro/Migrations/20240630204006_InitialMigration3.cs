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
            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[,]
                {
                    { 1, 2, 9 },
                    { 1, 3, 9 },
                    { 1, 4, 9 },
                    { 1, 5, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "Entity Framework");

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 6, "TypeScript" },
                    { 7, "CI/CD" },
                    { 8, "oAuth2" }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[,]
                {
                    { 1, 6, 8 },
                    { 1, 7, 9 },
                    { 1, 8, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "Entity");
        }
    }
}
