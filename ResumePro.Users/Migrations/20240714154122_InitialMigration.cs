using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResumePro.Users.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IdentityServer");

            migrationBuilder.CreateTable(
                name: "ApiResource",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AllowedAccessTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    RequireResourceIndicator = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiScope",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Emphasize = table.Column<bool>(type: "bit", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(type: "bit", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(type: "bit", nullable: false),
                    AllowRememberConsent = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(type: "bit", nullable: false),
                    RequirePkce = table.Column<bool>(type: "bit", nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(type: "bit", nullable: false),
                    RequireRequestObject = table.Column<bool>(type: "bit", nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(type: "bit", nullable: false),
                    RequireDPoP = table.Column<bool>(type: "bit", nullable: false),
                    DPoPValidationMode = table.Column<int>(type: "int", nullable: false),
                    DPoPClockSkew = table.Column<TimeSpan>(type: "time", nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                    BackChannelLogoutUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                    AllowOfflineAccess = table.Column<bool>(type: "bit", nullable: false),
                    IdentityTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AllowedIdentityTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccessTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    ConsentLifetime = table.Column<int>(type: "int", nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    RefreshTokenUsage = table.Column<int>(type: "int", nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenExpiration = table.Column<int>(type: "int", nullable: false),
                    AccessTokenType = table.Column<int>(type: "int", nullable: false),
                    EnableLocalLogin = table.Column<bool>(type: "bit", nullable: false),
                    IncludeJwtId = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(type: "bit", nullable: false),
                    ClientClaimsPrefix = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InitiateLoginUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UserSsoLifetime = table.Column<int>(type: "int", nullable: true),
                    UserCodeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    CibaLifetime = table.Column<int>(type: "int", nullable: true),
                    PollingInterval = table.Column<int>(type: "int", nullable: true),
                    CoordinateLifetimeWithUserSession = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false),
                    PushedAuthorizationLifetime = table.Column<int>(type: "int", nullable: true),
                    RequirePushedAuthorization = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFlowCodes",
                schema: "IdentityServer",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceFlowCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "IdentityProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResource",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Emphasize = table.Column<bool>(type: "bit", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Key",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Key", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PushedAuthorizationRequests",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceValueHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushedAuthorizationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerSideSession",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Scheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Renewed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSideSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceClaim",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceClaim_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "IdentityServer",
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceProperty",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceProperty_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "IdentityServer",
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceScope",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceScope_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "IdentityServer",
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceSecret",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiResourceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceSecret_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "IdentityServer",
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeClaim",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeClaim_ApiScope_ScopeId",
                        column: x => x.ScopeId,
                        principalSchema: "IdentityServer",
                        principalTable: "ApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeProperty",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeProperty_ApiScope_ScopeId",
                        column: x => x.ScopeId,
                        principalSchema: "IdentityServer",
                        principalTable: "ApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientClaim",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientClaim_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientCorsOrigin",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCorsOrigin_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGrantType",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientGrantType_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdPRestriction",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdPRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientIdPRestriction_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPostLogoutRedirectUri",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostLogoutRedirectUri = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPostLogoutRedirectUri_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProperty",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProperty_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRedirectUri",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RedirectUri = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRedirectUri_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScope",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientScope_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecret",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSecret_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "IdentityServer",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceClaim",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceClaim_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalSchema: "IdentityServer",
                        principalTable: "IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceProperty",
                schema: "IdentityServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityResourceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceProperty_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalSchema: "IdentityServer",
                        principalTable: "IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CurrentApplication = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.UserId, x.ProviderKey, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { 5, 2592000, 3600, 0, false, false, false, true, null, true, false, 300, true, null, null, "client_", "resumepro", "ResumePro", null, null, null, new DateTime(2021, 9, 18, 13, 12, 13, 653, DateTimeKind.Unspecified).AddTicks(7956), new TimeSpan(0, 0, 5, 0, 0), 0, null, 300, true, true, true, null, 300, true, null, null, null, false, null, null, "oidc", null, 1, 1, false, false, false, true, false, false, 1296000, false, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "IdentityServer",
                table: "IdentityResource",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Emphasize", "Enabled", "Name", "NonEditable", "Required", "ShowInDiscoveryDocument", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 17, 3, 58, 20, 206, DateTimeKind.Unspecified).AddTicks(3232), "Your user profile information (first name, last name, etc.)", "User profile", true, true, "profile", false, false, true, null },
                    { 2, new DateTime(2021, 9, 17, 3, 58, 20, 185, DateTimeKind.Unspecified).AddTicks(7082), null, "Your user identifier", false, true, "openid", false, true, true, null },
                    { 3, new DateTime(2021, 9, 17, 3, 58, 20, 185, DateTimeKind.Unspecified).AddTicks(7082), null, "Your organization identifier", false, true, "organization", false, true, true, null }
                });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Test Organization" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "20f1b6e7-64b7-4658-9f5a-ca9b73da374e", "admin", "ADMIN" },
                    { 2, "20f1b6e7-64b7-4658-9f5a-ca9b73da374e", "member", "MEMBER" }
                });

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
                    { 13, 5, "openid" },
                    { 14, 1, "organization" }
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
                    { 15, 2, "sub" },
                    { 16, 3, "organizationId" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CurrentApplication", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrganizationId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "4a1f1ee5-0ce2-4b5d-88be-4373574ef024", null, "admin@admin.com", true, "Rod", "Johnson", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", 1, "AQAAAAIAAYagAAAAEKWd/iQx36LYevmQZ7567wkLZT31FgSYJiEiNwdMi9oYappMoiWnbGCOZOsbO5325g==", "123-123-1234", false, "GHCMP3XRBQUGXXFNLNP4UCVZAHL73RZ6", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiResource_Name",
                schema: "IdentityServer",
                table: "ApiResource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceClaim_ApiResourceId_Type",
                schema: "IdentityServer",
                table: "ApiResourceClaim",
                columns: new[] { "ApiResourceId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceProperty_ApiResourceId_Key",
                schema: "IdentityServer",
                table: "ApiResourceProperty",
                columns: new[] { "ApiResourceId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceScope_ApiResourceId_Scope",
                schema: "IdentityServer",
                table: "ApiResourceScope",
                columns: new[] { "ApiResourceId", "Scope" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceSecret_ApiResourceId",
                schema: "IdentityServer",
                table: "ApiResourceSecret",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScope_Name",
                schema: "IdentityServer",
                table: "ApiScope",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeClaim_ScopeId_Type",
                schema: "IdentityServer",
                table: "ApiScopeClaim",
                columns: new[] { "ScopeId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeProperty_ScopeId_Key",
                schema: "IdentityServer",
                table: "ApiScopeProperty",
                columns: new[] { "ScopeId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientId",
                schema: "IdentityServer",
                table: "Client",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientClaim_ClientId_Type_Value",
                schema: "IdentityServer",
                table: "ClientClaim",
                columns: new[] { "ClientId", "Type", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientCorsOrigin_ClientId_Origin",
                schema: "IdentityServer",
                table: "ClientCorsOrigin",
                columns: new[] { "ClientId", "Origin" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientGrantType_ClientId_GrantType",
                schema: "IdentityServer",
                table: "ClientGrantType",
                columns: new[] { "ClientId", "GrantType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdPRestriction_ClientId_Provider",
                schema: "IdentityServer",
                table: "ClientIdPRestriction",
                columns: new[] { "ClientId", "Provider" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientPostLogoutRedirectUri_ClientId_PostLogoutRedirectUri",
                schema: "IdentityServer",
                table: "ClientPostLogoutRedirectUri",
                columns: new[] { "ClientId", "PostLogoutRedirectUri" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientProperty_ClientId_Key",
                schema: "IdentityServer",
                table: "ClientProperty",
                columns: new[] { "ClientId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientRedirectUri_ClientId_RedirectUri",
                schema: "IdentityServer",
                table: "ClientRedirectUri",
                columns: new[] { "ClientId", "RedirectUri" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientScope_ClientId_Scope",
                schema: "IdentityServer",
                table: "ClientScope",
                columns: new[] { "ClientId", "Scope" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecret_ClientId",
                schema: "IdentityServer",
                table: "ClientSecret",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFlowCodes_DeviceCode",
                schema: "IdentityServer",
                table: "DeviceFlowCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFlowCodes_Expiration",
                schema: "IdentityServer",
                table: "DeviceFlowCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResource_Name",
                schema: "IdentityServer",
                table: "IdentityResource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceClaim_IdentityResourceId_Type",
                schema: "IdentityServer",
                table: "IdentityResourceClaim",
                columns: new[] { "IdentityResourceId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceProperty_IdentityResourceId_Key",
                schema: "IdentityServer",
                table: "IdentityResourceProperty",
                columns: new[] { "IdentityResourceId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Key_Use",
                schema: "IdentityServer",
                table: "Key",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                schema: "IdentityServer",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                schema: "IdentityServer",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Key",
                schema: "IdentityServer",
                table: "PersistedGrants",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                schema: "IdentityServer",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                schema: "IdentityServer",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PushedAuthorizationRequests_ExpiresAtUtc",
                schema: "IdentityServer",
                table: "PushedAuthorizationRequests",
                column: "ExpiresAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_PushedAuthorizationRequests_ReferenceValueHash",
                schema: "IdentityServer",
                table: "PushedAuthorizationRequests",
                column: "ReferenceValueHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSession_DisplayName",
                schema: "IdentityServer",
                table: "ServerSideSession",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSession_Expires",
                schema: "IdentityServer",
                table: "ServerSideSession",
                column: "Expires");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSession_Key",
                schema: "IdentityServer",
                table: "ServerSideSession",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSession_SessionId",
                schema: "IdentityServer",
                table: "ServerSideSession",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSession_SubjectId",
                schema: "IdentityServer",
                table: "ServerSideSession",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganizationId",
                table: "User",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiResourceClaim",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ApiResourceProperty",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ApiResourceScope",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ApiResourceSecret",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ApiScopeClaim",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ApiScopeProperty",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientClaim",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientCorsOrigin",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientGrantType",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientIdPRestriction",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientPostLogoutRedirectUri",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientProperty",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientRedirectUri",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientScope",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ClientSecret",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "DeviceFlowCodes",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "IdentityProviders");

            migrationBuilder.DropTable(
                name: "IdentityResourceClaim",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "IdentityResourceProperty",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "Key",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "PersistedGrants",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "PushedAuthorizationRequests",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "ServerSideSession",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "ApiResource",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "ApiScope",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "IdentityResource",
                schema: "IdentityServer");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
