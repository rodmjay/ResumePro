#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;
using System.Collections.Generic;

namespace ResumePro.Users.Entities;

public class LocalClient
{
    public int Id { get; set; }
    public bool Enabled { get; set; } = true;
    public string ClientId { get; set; }
    public string ProtocolType { get; set; } = "oidc";
    public List<LocalClientSecret> ClientSecrets { get; set; }
    public bool RequireClientSecret { get; set; } = true;
    public string ClientName { get; set; }
    public string Description { get; set; }
    public string ClientUri { get; set; }
    public string LogoUri { get; set; }
    public bool RequireConsent { get; set; } = false;
    public bool AllowRememberConsent { get; set; } = true;
    public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
    public List<LocalClientGrantType> AllowedGrantTypes { get; set; }
    public bool RequirePkce { get; set; } = true;
    public bool AllowPlainTextPkce { get; set; }
    public bool RequireRequestObject { get; set; }
    public bool AllowAccessTokensViaBrowser { get; set; }
    public List<LocalClientRedirectUri> RedirectUris { get; set; }
    public List<LocalClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
    public string FrontChannelLogoutUri { get; set; }
    public bool FrontChannelLogoutSessionRequired { get; set; } = true;
    public string BackChannelLogoutUri { get; set; }
    public bool BackChannelLogoutSessionRequired { get; set; } = true;
    public bool AllowOfflineAccess { get; set; }
    public List<LocalClientScope> AllowedScopes { get; set; }
    public int IdentityTokenLifetime { get; set; } = 300;
    public string AllowedIdentityTokenSigningAlgorithms { get; set; }
    public int AccessTokenLifetime { get; set; } = 3600;
    public int AuthorizationCodeLifetime { get; set; } = 300;
    public int? ConsentLifetime { get; set; } = null;
    public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
    public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
    public int RefreshTokenUsage { get; set; } = 1;
    public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
    public int RefreshTokenExpiration { get; set; } = 1;
    public int AccessTokenType { get; set; }
    public bool EnableLocalLogin { get; set; } = true;
    public List<LocalClientIdPRestriction> IdentityProviderRestrictions { get; set; }
    public bool IncludeJwtId { get; set; }
    public List<LocalClientClaim> Claims { get; set; }
    public bool AlwaysSendClientClaims { get; set; }
    public string ClientClaimsPrefix { get; set; } = "client_";
    public string PairWiseSubjectSalt { get; set; }
    public List<LocalClientCorsOrigin> AllowedCorsOrigins { get; set; }
    public List<LocalClientProperty> Properties { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }
    public DateTime? LastAccessed { get; set; }
    public int? UserSsoLifetime { get; set; }
    public string UserCodeType { get; set; }
    public int DeviceCodeLifetime { get; set; } = 300;
    public bool NonEditable { get; set; }
}

public class LocalClientSecret
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    public DateTime? Expiration { get; set; }
    public string Type { get; set; } = "SharedSecret";
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public class LocalClientGrantType
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string GrantType { get; set; }
}

public class LocalClientRedirectUri
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string RedirectUri { get; set; }
}

public class LocalClientPostLogoutRedirectUri
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string PostLogoutRedirectUri { get; set; }
}

public class LocalClientScope
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string Scope { get; set; }
}

public class LocalClientIdPRestriction
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string Provider { get; set; }
}

public class LocalClientClaim
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}

public class LocalClientCorsOrigin
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string Origin { get; set; }
}

public class LocalClientProperty
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public LocalClient Client { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
