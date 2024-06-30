using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "JobId", "Description", "Order" },
                values: new object[,]
                {
                    { 1, 1, "BASF", 1 },
                    { 2, 1, "Microsoft", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumns: new[] { "Id", "JobId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Project");
        }
    }
}
