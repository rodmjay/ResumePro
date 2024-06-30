using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 18,
                column: "Text",
                value: "Responsible for architecture/development of components of banking platform");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "JobId", "ProjectId", "Text" },
                values: new object[] { 5, null, "Developed Fingerprint Login and Friends and Family Shared Banking components" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Order", "Text" },
                values: new object[] { 1, "Responsible for architecture/development of .net core backend" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Order", "Text" },
                values: new object[] { 2, "Developmed Complex medication and recurring activity scheduling" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Order", "Text" },
                values: new object[] { 3, "Made key technology decisions to keep project on time/budget" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Order", "ProjectId", "Text" },
                values: new object[] { 4, 3, "Maintained 90% Code Coverage" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Order", "Text" },
                values: new object[] { 1, "Responsible for entire backend and frontend architecture/development of .net core backend and angular front-end" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Order", "Text" },
                values: new object[] { 2, "Comprehensive single-entry accounting platform that calculates cashflow, tax savings, appreciation, and capital inflow" });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 26, 2, 4, 4, "Maintained 75% code coverage" },
                    { 27, 2, 3, 4, "Made key technology decisions to keep project on time/budget" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 18,
                column: "Text",
                value: "Architected complex, mission critical, authentication and authorization features for banking platform including: Fingerprint Login, Friends and Family Shared Banking");

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "JobId", "ProjectId", "Text" },
                values: new object[] { 2, 3, "Responsible for architecture/development of .net core backend" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Order", "Text" },
                values: new object[] { 2, "Developmed Complex medication and recurring activity scheduling" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Order", "Text" },
                values: new object[] { 3, "Made key technology decisions to keep project on time/budget" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Order", "Text" },
                values: new object[] { 4, "Maintained 90% Code Coverage" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Order", "ProjectId", "Text" },
                values: new object[] { 1, 4, "Responsible for entire backend and frontend development" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Order", "Text" },
                values: new object[] { 2, "Comprehensive single-entry accounting platform that calculates cashflow, tax savings, appreciation, and capital inflow" });

            migrationBuilder.UpdateData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Order", "Text" },
                values: new object[] { 3, "Maintained 75% code coverage" });
        }
    }
}
