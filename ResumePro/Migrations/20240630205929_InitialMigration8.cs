using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowInSummary",
                table: "ResumeSkill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 1 },
                column: "ShowInSummary",
                value: true);

            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 2 },
                column: "ShowInSummary",
                value: true);

            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 3 },
                column: "ShowInSummary",
                value: true);

            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 4 },
                column: "ShowInSummary",
                value: true);

            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 5 },
                column: "ShowInSummary",
                value: true);

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId", "ShowInSummary" },
                values: new object[,]
                {
                    { 1, 1, 6, false },
                    { 1, 1, 7, false },
                    { 1, 1, 8, false }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 9, "Visual Studio 20229" },
                    { 10, "Identity Server" },
                    { 11, "Postman" },
                    { 12, "SignalR" },
                    { 13, "Material Design" },
                    { 14, "Bootstrap" },
                    { 15, "Figma" },
                    { 16, "gRPC" },
                    { 17, "Blazor" }
                });

            migrationBuilder.InsertData(
                table: "PersonaSkill",
                columns: new[] { "PersonaId", "SkillId", "Rating" },
                values: new object[,]
                {
                    { 1, 9, 8 },
                    { 1, 10, 8 },
                    { 1, 11, 8 },
                    { 1, 12, 8 },
                    { 1, 13, 8 },
                    { 1, 14, 8 },
                    { 1, 15, 8 },
                    { 1, 16, 8 },
                    { 1, 17, 8 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSkill",
                columns: new[] { "PersonaId", "ResumeId", "SkillId", "ShowInSummary" },
                values: new object[,]
                {
                    { 1, 1, 9, false },
                    { 1, 1, 10, false },
                    { 1, 1, 11, false },
                    { 1, 1, 12, false },
                    { 1, 1, 13, false },
                    { 1, 1, 14, false },
                    { 1, 1, 15, false },
                    { 1, 1, 16, false },
                    { 1, 1, 17, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 6 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 7 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 8 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 9 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 10 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 11 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 12 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 13 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 14 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 15 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 16 });

            migrationBuilder.DeleteData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 17 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "PersonaSkill",
                keyColumns: new[] { "PersonaId", "SkillId" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DropColumn(
                name: "ShowInSummary",
                table: "ResumeSkill");
        }
    }
}
