using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class ClientAddedResumeBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "Client",
                columns: new[] { "Id", "AbsoluteRefreshTokenLifetime", "AccessTokenLifetime", "AccessTokenType", "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AllowPlainTextPkce", "AllowRememberConsent", "AllowedIdentityTokenSigningAlgorithms", "AlwaysIncludeUserClaimsInIdToken", "AlwaysSendClientClaims", "AuthorizationCodeLifetime", "BackChannelLogoutSessionRequired", "BackChannelLogoutUri", "CibaLifetime", "ClientClaimsPrefix", "ClientId", "ClientName", "ClientUri", "ConsentLifetime", "CoordinateLifetimeWithUserSession", "Created", "DPoPClockSkew", "DPoPValidationMode", "Description", "DeviceCodeLifetime", "EnableLocalLogin", "Enabled", "FrontChannelLogoutSessionRequired", "FrontChannelLogoutUri", "IdentityTokenLifetime", "IncludeJwtId", "InitiateLoginUri", "LastAccessed", "LogoUri", "NonEditable", "PairWiseSubjectSalt", "PollingInterval", "ProtocolType", "PushedAuthorizationLifetime", "RefreshTokenExpiration", "RefreshTokenUsage", "RequireClientSecret", "RequireConsent", "RequireDPoP", "RequirePkce", "RequirePushedAuthorization", "RequireRequestObject", "SlidingRefreshTokenLifetime", "UpdateAccessTokenClaimsOnRefresh", "Updated", "UserCodeType", "UserSsoLifetime" },
                values: new object[] { 6, 2592000, 3600, 0, false, false, false, true, null, true, false, 300, true, null, null, "client_", "resumebuilder", "ResumeBuilder", null, null, null, new DateTime(2021, 9, 18, 13, 12, 13, 653, DateTimeKind.Unspecified).AddTicks(7956), new TimeSpan(0, 0, 5, 0, 0), 0, null, 300, true, true, true, null, 300, true, null, null, null, false, null, null, "oidc", null, 1, 1, false, false, false, true, false, false, 1296000, false, null, null, null });

            migrationBuilder.UpdateData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 5,
                column: "RedirectUri",
                value: "http://localhost:7243/authentication/login-callback");

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                columns: new[] { "Id", "ClientId", "Origin" },
                values: new object[] { 5, 6, "https://localhost:44388" });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                columns: new[] { "Id", "ClientId", "GrantType" },
                values: new object[] { 6, 6, "authorization_code" });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                columns: new[] { "Id", "ClientId", "PostLogoutRedirectUri" },
                values: new object[] { 6, 6, "https://localhost:44388/authentication/logout-callback" });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                columns: new[] { "Id", "ClientId", "RedirectUri" },
                values: new object[] { 6, 6, "https://localhost:44388/authentication/login-callback" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientGrantType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "IdentityServer",
                table: "Client",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                keyColumn: "Id",
                keyValue: 5,
                column: "RedirectUri",
                value: "https://localhost:7243/authentication/login-callback");
        }
    }
}
