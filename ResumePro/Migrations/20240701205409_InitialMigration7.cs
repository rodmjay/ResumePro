using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: null);
        }
    }
}
