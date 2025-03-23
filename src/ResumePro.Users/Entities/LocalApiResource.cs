#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;
using System.Collections.Generic;

namespace ResumePro.Users.Entities;

public class LocalApiResource
{
    public int Id { get; set; }
    public bool Enabled { get; set; } = true;
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string AllowedAccessTokenSigningAlgorithms { get; set; }
    public bool ShowInDiscoveryDocument { get; set; } = true;
    public List<LocalApiResourceSecret> Secrets { get; set; }
    public List<LocalApiResourceScope> Scopes { get; set; }
    public List<LocalApiResourceClaim> UserClaims { get; set; }
    public List<LocalApiResourceProperty> Properties { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }
    public bool NonEditable { get; set; }
}

public class LocalApiResourceSecret
{
    public int Id { get; set; }
    public int ApiResourceId { get; set; }
    public LocalApiResource ApiResource { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    public DateTime? Expiration { get; set; }
    public string Type { get; set; } = "SharedSecret";
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public class LocalApiResourceScope
{
    public int Id { get; set; }
    public int ApiResourceId { get; set; }
    public LocalApiResource ApiResource { get; set; }
    public string Scope { get; set; }
}

public class LocalApiResourceClaim
{
    public int Id { get; set; }
    public int ApiResourceId { get; set; }
    public LocalApiResource ApiResource { get; set; }
    public string Type { get; set; }
}

public class LocalApiResourceProperty
{
    public int Id { get; set; }
    public int ApiResourceId { get; set; }
    public LocalApiResource ApiResource { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
