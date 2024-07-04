using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_SkillId",
                table: "JobSkill");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonaSkill_OrganizationId_SkillId",
                table: "PersonaSkill");

            migrationBuilder.AddColumn<int>(
                name: "PersonaId",
                table: "JobSkill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 1 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 2 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 2 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 3 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 4 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 4 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 4 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 4 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 5 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 5 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 5 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 6 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 6 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 6 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 7 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 7 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 7 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 8 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 8 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 8 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 8 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 9 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 9 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 9 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 10 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 10 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 10 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 11 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 11 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 12 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 12 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 13 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 13 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 14 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 16 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 17 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 18 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 19 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 20 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 21 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 22 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 22 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 22 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 23 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 24 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 25 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 25 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 26 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 27 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 28 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 29 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 30 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 31 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 31 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 32 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 33 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 33 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 34 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 5, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 36 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 37 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 1, 1, 38 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 38 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 39 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 40 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 41 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 42 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 4, 1, 42 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 43 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 43 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 43 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 2, 1, 44 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 3, 1, 44 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 6, 1, 44 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 7, 1, 44 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 44 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 8, 1, 46 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "JobSkill",
                keyColumns: new[] { "JobId", "OrganizationId", "SkillId" },
                keyValues: new object[] { 9, 1, 47 },
                column: "PersonaId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "PersonaId", "SkillId" },
                principalTable: "PersonaSkill",
                principalColumns: new[] { "OrganizationId", "PersonaId", "SkillId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill");

            migrationBuilder.DropIndex(
                name: "IX_JobSkill_OrganizationId_PersonaId_SkillId",
                table: "JobSkill");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "JobSkill");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonaSkill_OrganizationId_SkillId",
                table: "PersonaSkill",
                columns: new[] { "OrganizationId", "SkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_PersonaSkill_OrganizationId_SkillId",
                table: "JobSkill",
                columns: new[] { "OrganizationId", "SkillId" },
                principalTable: "PersonaSkill",
                principalColumns: new[] { "OrganizationId", "SkillId" });
        }
    }
}
