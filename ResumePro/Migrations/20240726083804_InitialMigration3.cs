using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Web Frameworks");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "DevOps/Infrastructure");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "APIs/Integration Tools");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Data Management/Analytics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Web Development Frameworks");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "DevOps and Infrastructure");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "APIs and Integration Tools");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Data Management and Analytics");
        }
    }
}
