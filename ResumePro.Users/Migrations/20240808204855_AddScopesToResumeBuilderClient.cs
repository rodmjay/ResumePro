using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class AddScopesToResumeBuilderClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientScope",
                columns: new[] { "Id", "ClientId", "Scope" },
                values: new object[,]
                {
                    { 15, 6, "api1" },
                    { 16, 6, "profile" },
                    { 17, 6, "openid" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
