using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "Id", "JobId", "Order", "ProjectId", "Text" },
                values: new object[,]
                {
                    { 12, 2, 1, 5, "Rebuild an antiquated system using the latest .NET Technologies (Blazor, .NET Core, gRPC)" },
                    { 13, 2, 2, 5, "Wired up existing SQL Server database to EF Core " },
                    { 14, 2, 2, 5, "Developed gRPC-based messaging system as per client request" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Highlight",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
