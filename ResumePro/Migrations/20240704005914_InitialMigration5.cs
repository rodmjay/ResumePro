using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 17 },
                column: "ShowInSummary",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ResumeSkill",
                keyColumns: new[] { "PersonaId", "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1, 17 },
                column: "ShowInSummary",
                value: false);
        }
    }
}
