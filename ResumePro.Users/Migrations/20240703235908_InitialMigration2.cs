using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "User");

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ApiScope",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Emphasize", "Enabled", "LastAccessed", "Name", "NonEditable", "Required", "ShowInDiscoveryDocument", "Updated" },
                values: new object[] { 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "My API", false, true, null, "api1", false, false, true, null });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "Client",
                columns: new[] { "Id", "AbsoluteRefreshTokenLifetime", "AccessTokenLifetime", "AccessTokenType", "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AllowPlainTextPkce", "AllowRememberConsent", "AllowedIdentityTokenSigningAlgorithms", "AlwaysIncludeUserClaimsInIdToken", "AlwaysSendClientClaims", "AuthorizationCodeLifetime", "BackChannelLogoutSessionRequired", "BackChannelLogoutUri", "CibaLifetime", "ClientClaimsPrefix", "ClientId", "ClientName", "ClientUri", "ConsentLifetime", "CoordinateLifetimeWithUserSession", "Created", "DPoPClockSkew", "DPoPValidationMode", "Description", "DeviceCodeLifetime", "EnableLocalLogin", "Enabled", "FrontChannelLogoutSessionRequired", "FrontChannelLogoutUri", "IdentityTokenLifetime", "IncludeJwtId", "InitiateLoginUri", "LastAccessed", "LogoUri", "NonEditable", "PairWiseSubjectSalt", "PollingInterval", "ProtocolType", "PushedAuthorizationLifetime", "RefreshTokenExpiration", "RefreshTokenUsage", "RequireClientSecret", "RequireConsent", "RequireDPoP", "RequirePkce", "RequirePushedAuthorization", "RequireRequestObject", "SlidingRefreshTokenLifetime", "UpdateAccessTokenClaimsOnRefresh", "Updated", "UserCodeType", "UserSsoLifetime" },
                values: new object[,]
                {
                    { 1, 2592000, 400000, 0, false, false, false, true, null, true, true, 300, true, null, null, "", "postman", null, null, null, null, new DateTime(2021, 9, 18, 13, 12, 13, 532, DateTimeKind.Unspecified).AddTicks(8105), new TimeSpan(0, 0, 5, 0, 0), 0, null, 300, true, true, true, null, 300, true, null, null, null, false, null, null, "oidc", null, 1, 1, true, false, false, true, false, false, 1296000, false, null, null, null },
                    { 2, 2592000, 3600, 0, false, false, false, true, null, true, true, 300, true, null, null, "client_", "client", null, null, null, null, new DateTime(2021, 9, 18, 13, 12, 13, 642, DateTimeKind.Unspecified).AddTicks(7421), new TimeSpan(0, 0, 5, 0, 0), 0, null, 300, true, true, true, null, 300, true, null, null, null, false, null, null, "oidc", null, 1, 1, true, false, false, true, false, false, 1296000, false, null, null, null },
                    { 3, 2592000, 3600, 0, false, false, false, true, null, true, true, 300, true, null, null, "client_", "mvc", null, null, null, null, new DateTime(2021, 9, 18, 13, 12, 13, 645, DateTimeKind.Unspecified).AddTicks(5968), new TimeSpan(0, 0, 5, 0, 0), 0, null, 300, true, true, true, null, 300, true, null, null, null, false, null, null, "oidc", null, 1, 1, true, false, false, true, false, false, 1296000, false, null, null, null },
                    { 5, 2592000, 3600, 0, false, false, false, true, null, true, false, 300, true, null, null, "client_", "resumepro", "TranslationPro", null, null, null, new DateTime(2021, 9, 18, 13, 12, 13, 653, DateTimeKind.Unspecified).AddTicks(7956), new TimeSpan(0, 0, 5, 0, 0), 0, null, 300, true, true, true, null, 300, true, null, null, null, false, null, null, "oidc", null, 1, 1, false, false, false, true, false, false, 1296000, false, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "IdentityResource",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Emphasize", "Enabled", "Name", "NonEditable", "Required", "ShowInDiscoveryDocument", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 17, 3, 58, 20, 206, DateTimeKind.Unspecified).AddTicks(3232), "Your user profile information (first name, last name, etc.)", "User profile", true, true, "profile", false, false, true, null },
                    { 2, new DateTime(2021, 9, 17, 3, 58, 20, 185, DateTimeKind.Unspecified).AddTicks(7082), null, "Your user identifier", false, true, "openid", false, true, true, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "20f1b6e7-64b7-4658-9f5a-ca9b73da374e", "admin", "ADMIN" },
                    { 2, "20f1b6e7-64b7-4658-9f5a-ca9b73da374e", "member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CurrentApplication", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "4a1f1ee5-0ce2-4b5d-88be-4373574ef024", null, "admin@admin.com", true, "Rod", "Johnson", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEKWd/iQx36LYevmQZ7567wkLZT31FgSYJiEiNwdMi9oYappMoiWnbGCOZOsbO5325g==", "123-123-1234", false, "GHCMP3XRBQUGXXFNLNP4UCVZAHL73RZ6", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                columns: new[] { "Id", "ClientId", "Origin" },
                values: new object[,]
                {
                    { 2, 5, "https://localhost:44330" },
                    { 3, 5, "https://resumepro-app-test.azurewebsites.net" },
                    { 4, 5, "https://localhost:7243" }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                columns: new[] { "Id", "ClientId", "GrantType" },
                values: new object[,]
                {
                    { 1, 1, "password" },
                    { 3, 2, "client_credentials" },
                    { 4, 3, "authorization_code" },
                    { 5, 5, "authorization_code" }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                columns: new[] { "Id", "ClientId", "PostLogoutRedirectUri" },
                values: new object[,]
                {
                    { 1, 3, "https://localhost:5002/signout-callback-oidc" },
                    { 3, 5, "https://localhost:44330/authentication/logout-callback" },
                    { 4, 5, "https://resumepro-app-test.azurewebsites.net/authentication/logout-callback" },
                    { 5, 5, "https://localhost:7243/authentication/logout-callback" }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                columns: new[] { "Id", "ClientId", "RedirectUri" },
                values: new object[,]
                {
                    { 1, 3, "https://localhost:5002/signin-oidc" },
                    { 3, 5, "https://localhost:44330/authentication/login-callback" },
                    { 4, 5, "https://resumepro-app-test.azurewebsites.net/authentication/login-callback" },
                    { 5, 5, "https://localhost:7243/authentication/login-callback" }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientScope",
                columns: new[] { "Id", "ClientId", "Scope" },
                values: new object[,]
                {
                    { 1, 3, "openid" },
                    { 2, 2, "api1" },
                    { 3, 3, "api1" },
                    { 4, 1, "api1" },
                    { 5, 1, "profile" },
                    { 6, 1, "openid" },
                    { 10, 3, "profile" },
                    { 11, 5, "api1" },
                    { 12, 5, "profile" },
                    { 13, 5, "openid" }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientSecret",
                columns: new[] { "Id", "ClientId", "Created", "Description", "Expiration", "Type", "Value" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 9, 17, 13, 19, 6, 414, DateTimeKind.Unspecified).AddTicks(3771), null, null, "SharedSecret", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=" },
                    { 2, 2, new DateTime(2021, 9, 17, 13, 19, 6, 564, DateTimeKind.Unspecified).AddTicks(8731), null, null, "SharedSecret", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=" },
                    { 3, 3, new DateTime(2021, 9, 17, 13, 19, 6, 568, DateTimeKind.Unspecified).AddTicks(1345), null, null, "SharedSecret", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=" }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                columns: new[] { "Id", "IdentityResourceId", "Type" },
                values: new object[,]
                {
                    { 1, 1, "website" },
                    { 2, 1, "picture" },
                    { 3, 1, "profile" },
                    { 4, 1, "preferred_username" },
                    { 5, 1, "nickname" },
                    { 6, 1, "middle_name" },
                    { 7, 1, "given_name" },
                    { 8, 1, "family_name" },
                    { 9, 1, "name" },
                    { 10, 1, "gender" },
                    { 11, 1, "birthdate" },
                    { 12, 1, "zoneinfo" },
                    { 13, 1, "locale" },
                    { 14, 1, "updated_at" },
                    { 15, 2, "sub" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ApiScope",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientScope",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientSecret",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientSecret",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientSecret",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResource",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "IdentityResource",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
