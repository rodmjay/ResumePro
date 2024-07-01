using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 4,
                column: "EndDate",
                value: new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 5,
                column: "EndDate",
                value: new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2013, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 8,
                column: "EndDate",
                value: new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { null, new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 4,
                column: "EndDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 5,
                column: "EndDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 8,
                column: "EndDate",
                value: new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
