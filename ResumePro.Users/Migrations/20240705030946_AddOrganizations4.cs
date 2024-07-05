using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientScope",
                columns: new[] { "Id", "ClientId", "Scope" },
                values: new object[] { 14, 1, "organization" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
