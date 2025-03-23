#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;

namespace ResumePro.Users.Entities;

public class LocalPersistedGrant
{
    public string Key { get; set; }
    public string Type { get; set; }
    public string SubjectId { get; set; }
    public string SessionId { get; set; }
    public string ClientId { get; set; }
    public string Description { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? ConsumedTime { get; set; }
    public string Data { get; set; }
}
