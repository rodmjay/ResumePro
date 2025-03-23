#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;

namespace ResumePro.Users.Entities;

public class LocalPushedAuthorizationRequest
{
    public string Id { get; set; }
    public string RequestUri { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public string Parameters { get; set; }
}
