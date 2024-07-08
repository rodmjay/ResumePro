using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SkillCategorySkill",
                columns: new[] { "SkillCategoryId", "SkillId" },
                values: new object[] { 3, 38 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 3, 38 });
        }
    }
}
