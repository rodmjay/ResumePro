using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "Company", "Description", "EndDate", "Location", "PersonaId", "StartDate", "Title" },
                values: new object[,]
                {
                    { 3, "IdeaFortune", "", null, "American Fork,UT", 1, new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Founder/Architect" },
                    { 4, "Agile Software", "", null, "", 1, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architect" },
                    { 5, "Access Softek", "", null, "West Jordan,UT", 1, new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sr. Engineer Dev Lead" }
                });

            migrationBuilder.InsertData(
                table: "ResumeJob",
                columns: new[] { "JobId", "PersonaId", "ResumeId" },
                values: new object[,]
                {
                    { 3, 1, 1 },
                    { 4, 1, 1 },
                    { 5, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeJob",
                keyColumns: new[] { "JobId", "PersonaId", "ResumeId" },
                keyValues: new object[] { 3, 1, 1 });

            migrationBuilder.DeleteData(
                table: "ResumeJob",
                keyColumns: new[] { "JobId", "PersonaId", "ResumeId" },
                keyValues: new object[] { 4, 1, 1 });

            migrationBuilder.DeleteData(
                table: "ResumeJob",
                keyColumns: new[] { "JobId", "PersonaId", "ResumeId" },
                keyValues: new object[] { 5, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Job",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
