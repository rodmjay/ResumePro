using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClientName",
                value: "ResumePro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClientName",
                value: "TranslationPro");
        }
    }
}
