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
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Company", "Description", "Title" },
                values: new object[] { "InfoSys", "", "Technical Architect" });

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Company", "Description", "Location", "Title" },
                values: new object[] { "Solution Stream", "", "American Fork,UT", "Sr. Software Architect" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Company", "Description", "Title" },
                values: new object[] { "X Co", "Test Description", "Architect" });

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Company", "Description", "Location", "Title" },
                values: new object[] { "Y Co", "Test Description", "Salt Lake City", "Architect" });
        }
    }
}
