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
            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 1, 28 });

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 28);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[] { 28, "Bluetooth Low Energy (BLE)" });

            migrationBuilder.InsertData(
                table: "SkillCategorySkill",
                columns: new[] { "SkillCategoryId", "SkillId" },
                values: new object[] { 1, 28 });
        }
    }
}
