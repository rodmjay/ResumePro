#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;

namespace ResumePro.Users.Entities;

public class LocalIdentityProvider
{
    public int Id { get; set; }
    public string Scheme { get; set; }
    public string DisplayName { get; set; }
    public bool Enabled { get; set; } = true;
    public string Type { get; set; }
    public string Properties { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? LastAccessed { get; set; }
    public bool NonEditable { get; set; }
}
