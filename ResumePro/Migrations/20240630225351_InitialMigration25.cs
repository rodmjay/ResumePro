using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 12,
                column: "Text",
                value: "Responsible for re-architecture and development of an antiquated system using the latest .NET Technologies");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Order", "Text" },
                values: new object[] { 3, "Developed gRPC-based messaging system" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 19,
                column: "Text",
                value: "Responsible for architecture/development of .net core backend");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 20,
                column: "Text",
                value: "Developmed Complex medication and recurring activity scheduling");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 21,
                column: "Text",
                value: "Made key technology decisions to keep project on time/budget");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 22,
                column: "Text",
                value: "Maintained 90% Code Coverage");

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 23, 2, 1, 4, "Responsible for entire backend and frontend development" },
                    { 24, 2, 2, 4, "Comprehensive single-entry accounting platform that calculates cashflow, tax savings, appreciation, and capital inflow" },
                    { 25, 2, 3, 4, "Maintained 75% code coverage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 12,
                column: "Text",
                value: "Rebuild an antiquated system using the latest .NET Technologies (Blazor, .NET Core, gRPC)");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Order", "Text" },
                values: new object[] { 2, "Developed gRPC-based messaging system as per client request" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 19,
                column: "Text",
                value: "Responsible for architecture/development of entire backend");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 20,
                column: "Text",
                value: "Complex medication and recurring activity scheduling");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 21,
                column: "Text",
                value: "Made technology decisions to keep project on budget");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 22,
                column: "Text",
                value: "Maintained 90% code coverage");
        }
    }
}
