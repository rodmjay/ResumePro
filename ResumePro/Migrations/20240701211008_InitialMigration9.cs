using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Robert Clymer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reference",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Rob Atlas");
        }
    }
}
