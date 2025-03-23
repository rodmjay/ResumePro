#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;
using System.Collections.Generic;

namespace ResumePro.Users.Entities;

public class LocalApiScope
{
    public int Id { get; set; }
    public bool Enabled { get; set; } = true;
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public bool Required { get; set; }
    public bool Emphasize { get; set; }
    public bool ShowInDiscoveryDocument { get; set; } = true;
    public List<LocalApiScopeClaim> UserClaims { get; set; }
    public List<LocalApiScopeProperty> Properties { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }
    public bool NonEditable { get; set; }
}

public class LocalApiScopeClaim
{
    public int Id { get; set; }
    public int ScopeId { get; set; }
    public LocalApiScope Scope { get; set; }
    public string Type { get; set; }
}

public class LocalApiScopeProperty
{
    public int Id { get; set; }
    public int ScopeId { get; set; }
    public LocalApiScope Scope { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
