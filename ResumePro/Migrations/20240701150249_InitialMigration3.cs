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
            migrationBuilder.DeleteData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reference",
                columns: new[] { "Id", "JobId", "Name", "PhoneNumber", "Text" },
                values: new object[] { 3, 2, "Test1", "Test2", "We asked people to step up and share their ideas and opinions on how to make SolutionStream more successful, Rod was one of the first people to come forward with an idea and plan and he executed it flawlessly. He is consistently bringing forward new ideas and ways to innovate some of the processes here, from interviewing to development" });
        }
    }
}
