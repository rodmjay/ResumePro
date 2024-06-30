using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "SkillId", "ResumeId" },
                values: new object[,]
                {
                    { 2, 2, 1 },
                    { 2, 3, 1 },
                    { 2, 4, 1 },
                    { 2, 5, 1 },
                    { 2, 6, 1 },
                    { 2, 7, 1 },
                    { 2, 8, 1 },
                    { 2, 9, 1 },
                    { 2, 10, 1 },
                    { 2, 11, 1 },
                    { 2, 16, 1 },
                    { 2, 17, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 11 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "SkillId" },
                keyValues: new object[] { 2, 17 });
        }
    }
}
