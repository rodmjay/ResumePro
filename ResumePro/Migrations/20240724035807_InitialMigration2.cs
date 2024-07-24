using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 1, 33 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 1, 36 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 1, 45 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 3, 26 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 6, 39 });

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SkillCategory");

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
                keyValue: 7,
                column: "Name",
                value: "Software Development Tools");

            migrationBuilder.InsertData(
                table: "SkillCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "UI/UX Design" },
                    { 9, "Data Management and Analytics" },
                    { 10, "Server-Side Technologies" },
                    { 11, "Markup Languages" },
                    { 12, "Payment and Commerce" },
                    { 13, "Legacy Technologies" }
                });

            migrationBuilder.InsertData(
                table: "SkillCategorySkill",
                columns: new[] { "SkillCategoryId", "SkillId" },
                values: new object[,]
                {
                    { 1, 28 },
                    { 3, 43 },
                    { 6, 8 },
                    { 6, 10 },
                    { 6, 11 },
                    { 6, 25 },
                    { 7, 41 },
                    { 7, 42 },
                    { 7, 45 },
                    { 8, 13 },
                    { 8, 14 },
                    { 8, 15 },
                    { 9, 19 },
                    { 9, 20 },
                    { 9, 23 },
                    { 9, 35 },
                    { 9, 37 },
                    { 10, 5 },
                    { 10, 12 },
                    { 10, 16 },
                    { 10, 18 },
                    { 10, 21 },
                    { 10, 22 },
                    { 10, 26 },
                    { 10, 29 },
                    { 10, 30 },
                    { 11, 36 },
                    { 11, 47 },
                    { 12, 28 },
                    { 12, 39 },
                    { 12, 40 },
                    { 13, 31 },
                    { 13, 32 },
                    { 13, 33 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 1, 28 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 3, 43 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 6, 8 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 6, 10 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 6, 11 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 6, 25 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 7, 41 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 7, 42 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 7, 45 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 8, 13 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 8, 14 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 8, 15 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 9, 19 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 9, 20 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 9, 23 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 9, 35 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 9, 37 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 12 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 16 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 18 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 21 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 22 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 26 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 29 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 10, 30 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 11, 36 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 11, 47 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 12, 28 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 12, 39 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 12, 40 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 13, 31 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 13, 32 });

            migrationBuilder.DeleteData(
                table: "SkillCategorySkill",
                keyColumns: new[] { "SkillCategoryId", "SkillId" },
                keyValues: new object[] { 13, 33 });

            migrationBuilder.DeleteData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SkillCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Languages used for programming");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Database management systems");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Software frameworks", "Frameworks" });

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Tools for DevOps and CI/CD", "DevOps Tools" });

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Platforms for cloud computing");

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Application Programming Interfaces", "APIs" });

            migrationBuilder.UpdateData(
                table: "SkillCategory",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Integrated Development Environment", "IDEs" });

            migrationBuilder.InsertData(
                table: "SkillCategorySkill",
                columns: new[] { "SkillCategoryId", "SkillId" },
                values: new object[,]
                {
                    { 1, 33 },
                    { 1, 36 },
                    { 1, 45 },
                    { 3, 5 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 26 },
                    { 6, 39 }
                });
        }
    }
}
