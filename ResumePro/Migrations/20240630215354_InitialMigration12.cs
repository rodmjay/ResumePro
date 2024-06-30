using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjectId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjectId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjectId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProjectId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProjectId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "JobId", "Description", "Order" },
                values: new object[,]
                {
                    { 3, 2, "Elder Care Management Platform", 1 },
                    { 4, 2, "Project 2: Real Estate Accounting Platform", 2 },
                    { 5, 2, "Project 3: Major Food Distribution System ($300k)", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProjectId",
                value: null);
        }
    }
}
