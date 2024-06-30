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
            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ShortName" },
                values: new object[] { "Rod is an enterprise architect with expertise in the latest .NET and web technologies.  Rod has 19 years of experience as a professional developer and architect, with expertise in the complete software development lifecycle from ideation to implementation.  Rod is consistently described as a 10x developer who delivers high-end software from the ground up.", "Enterprice Application Architect" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Title" },
                values: new object[] { 5, "Entity" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Resume",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ShortName" },
                values: new object[] { "Hello There", "C# Developer" });
        }
    }
}
