#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Core.Data.Bases;
using ResumePro.Users.Entities;
using ResumePro.Users.Seeding.Extensions;

namespace ResumePro.Users.Contexts;

public class ApplicationContext : BaseContext<ApplicationContext>
{
    private static readonly string IdentityServerSchema = "IdentityServer";
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationContext(
        DbContextOptions<ApplicationContext> options, ILoggerFactory loggerFactory) :
        base(options)
    {
        _loggerFactory = loggerFactory;
    }


    public ApplicationContext(
        DbContextOptions<ApplicationContext> options) : this(options, null)
    {
    }

    public DbSet<LocalClient> Clients { get; set; }
    public DbSet<LocalClientCorsOrigin> ClientCorsOrigins { get; set; }
    public DbSet<LocalIdentityResource> IdentityResources { get; set; }
    public DbSet<LocalApiResource> ApiResources { get; set; }
    public DbSet<LocalApiScope> ApiScopes { get; set; }
    public DbSet<LocalIdentityProvider> IdentityProviders { get; set; }
    public DbSet<LocalPersistedGrant> PersistedGrants { get; set; }
    public DbSet<LocalDeviceFlowCodes> DeviceFlowCodes { get; set; }
    public DbSet<LocalKey> Keys { get; set; }
    public DbSet<LocalServerSideSession> ServerSideSessions { get; set; }
    public DbSet<LocalPushedAuthorizationRequest> PushedAuthorizationRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_loggerFactory != null) optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    protected override void ConfigureDatabase(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        // Configure local entity tables with the same schema
        builder.Entity<LocalClient>().ToTable("Client", IdentityServerSchema);
        builder.Entity<LocalClientSecret>().ToTable("ClientSecret", IdentityServerSchema);
        builder.Entity<LocalClientGrantType>().ToTable("ClientGrantType", IdentityServerSchema);
        builder.Entity<LocalClientRedirectUri>().ToTable("ClientRedirectUri", IdentityServerSchema);
        builder.Entity<LocalClientPostLogoutRedirectUri>().ToTable("ClientPostLogoutRedirectUri", IdentityServerSchema);
        builder.Entity<LocalClientScope>().ToTable("ClientScope", IdentityServerSchema);
        builder.Entity<LocalClientIdPRestriction>().ToTable("ClientIdPRestriction", IdentityServerSchema);
        builder.Entity<LocalClientClaim>().ToTable("ClientClaim", IdentityServerSchema);
        builder.Entity<LocalClientCorsOrigin>().ToTable("ClientCorsOrigin", IdentityServerSchema);
        builder.Entity<LocalClientProperty>().ToTable("ClientProperty", IdentityServerSchema);

        builder.Entity<LocalIdentityResource>().ToTable("IdentityResource", IdentityServerSchema);
        builder.Entity<LocalIdentityResourceClaim>().ToTable("IdentityResourceClaim", IdentityServerSchema);
        builder.Entity<LocalIdentityResourceProperty>().ToTable("IdentityResourceProperty", IdentityServerSchema);

        builder.Entity<LocalApiResource>().ToTable("ApiResource", IdentityServerSchema);
        builder.Entity<LocalApiResourceSecret>().ToTable("ApiResourceSecret", IdentityServerSchema);
        builder.Entity<LocalApiResourceScope>().ToTable("ApiResourceScope", IdentityServerSchema);
        builder.Entity<LocalApiResourceClaim>().ToTable("ApiResourceClaim", IdentityServerSchema);
        builder.Entity<LocalApiResourceProperty>().ToTable("ApiResourceProperty", IdentityServerSchema);

        builder.Entity<LocalApiScope>().ToTable("ApiScope", IdentityServerSchema);
        builder.Entity<LocalApiScopeClaim>().ToTable("ApiScopeClaim", IdentityServerSchema);
        builder.Entity<LocalApiScopeProperty>().ToTable("ApiScopeProperty", IdentityServerSchema);

        builder.Entity<LocalIdentityProvider>().ToTable("IdentityProvider", IdentityServerSchema);

        builder.Entity<LocalPersistedGrant>().ToTable("PersistedGrants", IdentityServerSchema);
        builder.Entity<LocalDeviceFlowCodes>().ToTable("DeviceFlowCodes", IdentityServerSchema);
        builder.Entity<LocalKey>().ToTable("Key", IdentityServerSchema);
        builder.Entity<LocalServerSideSession>().ToTable("ServerSideSession", IdentityServerSchema);
        builder.Entity<LocalPushedAuthorizationRequest>().ToTable("PushedAuthorizationRequests", IdentityServerSchema);

        // Configure relationships
        builder.Entity<LocalClientSecret>()
            .HasOne(s => s.Client)
            .WithMany(c => c.ClientSecrets)
            .HasForeignKey(s => s.ClientId);

        builder.Entity<LocalClientGrantType>()
            .HasOne(gt => gt.Client)
            .WithMany(c => c.AllowedGrantTypes)
            .HasForeignKey(gt => gt.ClientId);

        builder.Entity<LocalClientRedirectUri>()
            .HasOne(ru => ru.Client)
            .WithMany(c => c.RedirectUris)
            .HasForeignKey(ru => ru.ClientId);

        builder.Entity<LocalClientPostLogoutRedirectUri>()
            .HasOne(plru => plru.Client)
            .WithMany(c => c.PostLogoutRedirectUris)
            .HasForeignKey(plru => plru.ClientId);

        builder.Entity<LocalClientScope>()
            .HasOne(s => s.Client)
            .WithMany(c => c.AllowedScopes)
            .HasForeignKey(s => s.ClientId);

        builder.Entity<LocalClientIdPRestriction>()
            .HasOne(ipr => ipr.Client)
            .WithMany(c => c.IdentityProviderRestrictions)
            .HasForeignKey(ipr => ipr.ClientId);

        builder.Entity<LocalClientClaim>()
            .HasOne(c => c.Client)
            .WithMany(cl => cl.Claims)
            .HasForeignKey(c => c.ClientId);

        builder.Entity<LocalClientCorsOrigin>()
            .HasOne(co => co.Client)
            .WithMany(c => c.AllowedCorsOrigins)
            .HasForeignKey(co => co.ClientId);

        builder.Entity<LocalClientProperty>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Properties)
            .HasForeignKey(p => p.ClientId);

        builder.Entity<LocalIdentityResourceClaim>()
            .HasOne(c => c.IdentityResource)
            .WithMany(ir => ir.UserClaims)
            .HasForeignKey(c => c.IdentityResourceId);

        builder.Entity<LocalIdentityResourceProperty>()
            .HasOne(p => p.IdentityResource)
            .WithMany(ir => ir.Properties)
            .HasForeignKey(p => p.IdentityResourceId);

        builder.Entity<LocalApiResourceSecret>()
            .HasOne(s => s.ApiResource)
            .WithMany(ar => ar.Secrets)
            .HasForeignKey(s => s.ApiResourceId);

        builder.Entity<LocalApiResourceScope>()
            .HasOne(s => s.ApiResource)
            .WithMany(ar => ar.Scopes)
            .HasForeignKey(s => s.ApiResourceId);

        builder.Entity<LocalApiResourceClaim>()
            .HasOne(c => c.ApiResource)
            .WithMany(ar => ar.UserClaims)
            .HasForeignKey(c => c.ApiResourceId);

        builder.Entity<LocalApiResourceProperty>()
            .HasOne(p => p.ApiResource)
            .WithMany(ar => ar.Properties)
            .HasForeignKey(p => p.ApiResourceId);

        builder.Entity<LocalApiScopeClaim>()
            .HasOne(c => c.Scope)
            .WithMany(s => s.UserClaims)
            .HasForeignKey(c => c.ScopeId);

        builder.Entity<LocalApiScopeProperty>()
            .HasOne(p => p.Scope)
            .WithMany(s => s.Properties)
            .HasForeignKey(p => p.ScopeId);
    }

    private void SeedIdentityServer(ModelBuilder builder)
    {
        // Seeding is disabled since there are no seed files.
    }

    private void SeedUsersAndRoles(ModelBuilder builder)
    {
        builder.Entity<Organization>().Seed("organizations.csv");
        builder.Entity<User>().Seed("users.csv");
        builder.Entity<Role>().Seed("roles.csv");
        builder.Entity<UserRole>().Seed("userRoles.csv");
    }

    protected override void SeedDatabase(ModelBuilder builder)
    {
        // these should be placed in the Seeding/csv folder for it to work
        // make sure files are marked as "EmbeddedResource => Copy if newer"

        SeedIdentityServer(builder);
        SeedUsersAndRoles(builder);
    }
}
