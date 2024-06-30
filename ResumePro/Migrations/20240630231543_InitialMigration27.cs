using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 18,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 19,
                column: "Order",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 20,
                column: "Order",
                value: 4);

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "Company", "Description", "EndDate", "Location", "PersonaId", "StartDate", "Title" },
                values: new object[,]
                {
                    { 6, "NETCHEX", "", new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Louisiana", 1, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect Consultant" },
                    { 7, "Ancestry.com", "", new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provo,UT", 1, new DateTime(2010, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Engineer" },
                    { 8, "Cathexis", "", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provo,UT", 1, new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect/Dev Manager" },
                    { 9, "Motorola Public Safety", "", new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salt Lake City,UT", 1, new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 18,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 19,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 20,
                column: "Order",
                value: 1);
        }
    }
}
