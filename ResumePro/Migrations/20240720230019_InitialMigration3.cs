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
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkill",
                table: "JobSkill");

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 2 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 2 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 11 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 11 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 12 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 12 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 13 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 13 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 14 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 16 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 17 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 18 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 19 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 20 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 21 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 23 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 24 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 25 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 25 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 26 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 27 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 28 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 29 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 30 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 31 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 31 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 32 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 33 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 33 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 37 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 38 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 38 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 39 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 40 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 41 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 42 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 42 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 43 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 43 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 43 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 46 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 47 });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkill",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "PersonaId", "JobId", "SkillId" });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 3 },
                    { 1, 1, 1, 4 },
                    { 1, 1, 1, 5 },
                    { 1, 1, 1, 6 },
                    { 1, 1, 1, 7 },
                    { 1, 1, 1, 9 },
                    { 1, 1, 1, 18 },
                    { 1, 1, 1, 19 },
                    { 1, 1, 1, 20 },
                    { 1, 1, 1, 21 },
                    { 1, 1, 1, 22 },
                    { 1, 1, 1, 23 },
                    { 1, 1, 1, 24 },
                    { 1, 1, 1, 31 },
                    { 1, 1, 1, 34 },
                    { 1, 1, 1, 36 },
                    { 1, 1, 1, 37 },
                    { 1, 1, 1, 38 },
                    { 2, 1, 1, 1 },
                    { 2, 1, 1, 2 },
                    { 2, 1, 1, 3 },
                    { 2, 1, 1, 4 },
                    { 2, 1, 1, 5 },
                    { 2, 1, 1, 6 },
                    { 2, 1, 1, 7 },
                    { 2, 1, 1, 8 },
                    { 2, 1, 1, 9 },
                    { 2, 1, 1, 10 },
                    { 2, 1, 1, 11 },
                    { 2, 1, 1, 12 },
                    { 2, 1, 1, 13 },
                    { 2, 1, 1, 14 },
                    { 2, 1, 1, 16 },
                    { 2, 1, 1, 17 },
                    { 2, 1, 1, 22 },
                    { 2, 1, 1, 34 },
                    { 2, 1, 1, 36 },
                    { 2, 1, 1, 38 },
                    { 2, 1, 1, 43 },
                    { 2, 1, 1, 44 },
                    { 3, 1, 1, 1 },
                    { 3, 1, 1, 2 },
                    { 3, 1, 1, 3 },
                    { 3, 1, 1, 4 },
                    { 3, 1, 1, 5 },
                    { 3, 1, 1, 6 },
                    { 3, 1, 1, 7 },
                    { 3, 1, 1, 8 },
                    { 3, 1, 1, 9 },
                    { 3, 1, 1, 10 },
                    { 3, 1, 1, 11 },
                    { 3, 1, 1, 12 },
                    { 3, 1, 1, 13 },
                    { 3, 1, 1, 22 },
                    { 3, 1, 1, 34 },
                    { 3, 1, 1, 36 },
                    { 3, 1, 1, 39 },
                    { 3, 1, 1, 40 },
                    { 3, 1, 1, 41 },
                    { 3, 1, 1, 42 },
                    { 3, 1, 1, 44 },
                    { 4, 1, 1, 1 },
                    { 4, 1, 1, 3 },
                    { 4, 1, 1, 4 },
                    { 4, 1, 1, 8 },
                    { 4, 1, 1, 10 },
                    { 4, 1, 1, 25 },
                    { 4, 1, 1, 31 },
                    { 4, 1, 1, 36 },
                    { 4, 1, 1, 42 },
                    { 5, 1, 1, 1 },
                    { 5, 1, 1, 3 },
                    { 5, 1, 1, 8 },
                    { 5, 1, 1, 25 },
                    { 5, 1, 1, 26 },
                    { 5, 1, 1, 27 },
                    { 5, 1, 1, 28 },
                    { 5, 1, 1, 36 },
                    { 6, 1, 1, 1 },
                    { 6, 1, 1, 3 },
                    { 6, 1, 1, 34 },
                    { 6, 1, 1, 36 },
                    { 6, 1, 1, 43 },
                    { 6, 1, 1, 44 },
                    { 7, 1, 1, 1 },
                    { 7, 1, 1, 34 },
                    { 7, 1, 1, 36 },
                    { 7, 1, 1, 43 },
                    { 7, 1, 1, 44 },
                    { 8, 1, 1, 1 },
                    { 8, 1, 1, 3 },
                    { 8, 1, 1, 33 },
                    { 8, 1, 1, 34 },
                    { 8, 1, 1, 36 },
                    { 8, 1, 1, 46 },
                    { 9, 1, 1, 1 },
                    { 9, 1, 1, 29 },
                    { 9, 1, 1, 30 },
                    { 9, 1, 1, 32 },
                    { 9, 1, 1, 33 },
                    { 9, 1, 1, 34 },
                    { 9, 1, 1, 36 },
                    { 9, 1, 1, 44 },
                    { 9, 1, 1, 47 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkill",
                table: "JobSkill");

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 18 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 19 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 20 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 21 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 23 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 24 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 31 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 37 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 1, 1, 38 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 2 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 11 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 12 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 13 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 14 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 16 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 17 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 38 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 43 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 2, 1, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 2 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 7 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 11 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 12 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 13 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 22 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 39 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 40 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 41 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 42 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 3, 1, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 10 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 25 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 31 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 4, 1, 1, 42 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 8 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 25 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 26 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 27 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 28 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 5, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 6, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 6, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 6, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 6, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 6, 1, 1, 43 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 6, 1, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 7, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 7, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 7, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 7, 1, 1, 43 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 7, 1, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 8, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 8, 1, 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 8, 1, 1, 33 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 8, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 8, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 8, 1, 1, 46 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 29 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 30 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 32 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 33 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 34 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 36 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 44 });

            migrationBuilder.DeleteData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "PersonaId", "SkillId" },
                keyValues: new object[] { 9, 1, 1, 47 });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkill",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "SkillId", "JobId" });

            migrationBuilder.InsertData(
                table: "JobSkill",
                columns: new[] { "JobId", "OrganizationId", "SkillId", "PersonaId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 1, 1 },
                    { 3, 1, 1, 1 },
                    { 4, 1, 1, 1 },
                    { 5, 1, 1, 1 },
                    { 6, 1, 1, 1 },
                    { 7, 1, 1, 1 },
                    { 8, 1, 1, 1 },
                    { 9, 1, 1, 1 },
                    { 2, 1, 2, 1 },
                    { 3, 1, 2, 1 },
                    { 1, 1, 3, 1 },
                    { 2, 1, 3, 1 },
                    { 3, 1, 3, 1 },
                    { 4, 1, 3, 1 },
                    { 5, 1, 3, 1 },
                    { 6, 1, 3, 1 },
                    { 8, 1, 3, 1 },
                    { 1, 1, 4, 1 },
                    { 2, 1, 4, 1 },
                    { 3, 1, 4, 1 },
                    { 4, 1, 4, 1 },
                    { 1, 1, 5, 1 },
                    { 2, 1, 5, 1 },
                    { 3, 1, 5, 1 },
                    { 1, 1, 6, 1 },
                    { 2, 1, 6, 1 },
                    { 3, 1, 6, 1 },
                    { 1, 1, 7, 1 },
                    { 2, 1, 7, 1 },
                    { 3, 1, 7, 1 },
                    { 2, 1, 8, 1 },
                    { 3, 1, 8, 1 },
                    { 4, 1, 8, 1 },
                    { 5, 1, 8, 1 },
                    { 1, 1, 9, 1 },
                    { 2, 1, 9, 1 },
                    { 3, 1, 9, 1 },
                    { 2, 1, 10, 1 },
                    { 3, 1, 10, 1 },
                    { 4, 1, 10, 1 },
                    { 2, 1, 11, 1 },
                    { 3, 1, 11, 1 },
                    { 2, 1, 12, 1 },
                    { 3, 1, 12, 1 },
                    { 2, 1, 13, 1 },
                    { 3, 1, 13, 1 },
                    { 2, 1, 14, 1 },
                    { 2, 1, 16, 1 },
                    { 2, 1, 17, 1 },
                    { 1, 1, 18, 1 },
                    { 1, 1, 19, 1 },
                    { 1, 1, 20, 1 },
                    { 1, 1, 21, 1 },
                    { 1, 1, 22, 1 },
                    { 2, 1, 22, 1 },
                    { 3, 1, 22, 1 },
                    { 1, 1, 23, 1 },
                    { 1, 1, 24, 1 },
                    { 4, 1, 25, 1 },
                    { 5, 1, 25, 1 },
                    { 5, 1, 26, 1 },
                    { 5, 1, 27, 1 },
                    { 5, 1, 28, 1 },
                    { 9, 1, 29, 1 },
                    { 9, 1, 30, 1 },
                    { 1, 1, 31, 1 },
                    { 4, 1, 31, 1 },
                    { 9, 1, 32, 1 },
                    { 8, 1, 33, 1 },
                    { 9, 1, 33, 1 },
                    { 1, 1, 34, 1 },
                    { 2, 1, 34, 1 },
                    { 3, 1, 34, 1 },
                    { 6, 1, 34, 1 },
                    { 7, 1, 34, 1 },
                    { 8, 1, 34, 1 },
                    { 9, 1, 34, 1 },
                    { 1, 1, 36, 1 },
                    { 2, 1, 36, 1 },
                    { 3, 1, 36, 1 },
                    { 4, 1, 36, 1 },
                    { 5, 1, 36, 1 },
                    { 6, 1, 36, 1 },
                    { 7, 1, 36, 1 },
                    { 8, 1, 36, 1 },
                    { 9, 1, 36, 1 },
                    { 1, 1, 37, 1 },
                    { 1, 1, 38, 1 },
                    { 2, 1, 38, 1 },
                    { 3, 1, 39, 1 },
                    { 3, 1, 40, 1 },
                    { 3, 1, 41, 1 },
                    { 3, 1, 42, 1 },
                    { 4, 1, 42, 1 },
                    { 2, 1, 43, 1 },
                    { 6, 1, 43, 1 },
                    { 7, 1, 43, 1 },
                    { 2, 1, 44, 1 },
                    { 3, 1, 44, 1 },
                    { 6, 1, 44, 1 },
                    { 7, 1, 44, 1 },
                    { 9, 1, 44, 1 },
                    { 8, 1, 46, 1 },
                    { 9, 1, 47, 1 }
                });
        }
    }
}
