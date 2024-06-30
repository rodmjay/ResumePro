using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolName",
                table: "School",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "School",
                columns: new[] { "Id", "EndDate", "Name", "PersonaId", "StartDate" },
                values: new object[] { 1, new DateTime(2005, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portland Community College", 1, new DateTime(2004, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Degree",
                columns: new[] { "Id", "Name", "SchoolId" },
                values: new object[] { 1, "AAS Computer and Information Systems", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Degree",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "School",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "School",
                newName: "SchoolName");
        }
    }
}
