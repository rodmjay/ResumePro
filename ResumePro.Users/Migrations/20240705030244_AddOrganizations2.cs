using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ApiScope",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Emphasize", "Enabled", "LastAccessed", "Name", "NonEditable", "Required", "ShowInDiscoveryDocument", "Updated" },
                values: new object[] { 2, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Organization", false, true, null, "organization", false, false, true, null });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "IdentityResource",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Emphasize", "Enabled", "Name", "NonEditable", "Required", "ShowInDiscoveryDocument", "Updated" },
                values: new object[] { 3, new DateTime(2021, 9, 17, 3, 58, 20, 185, DateTimeKind.Unspecified).AddTicks(7082), null, "Your organization identifier", false, true, "organization", false, true, true, null });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                columns: new[] { "Id", "IdentityResourceId", "Type" },
                values: new object[] { 16, 3, "organizationId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ApiScope",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResource",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
