#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;

namespace ResumePro.Users.Entities;

public class LocalKey
{
    public string Id { get; set; }
    public int Version { get; set; }
    public DateTime Created { get; set; }
    public string Use { get; set; }
    public string Algorithm { get; set; }
    public bool IsX509Certificate { get; set; }
    public bool DataProtected { get; set; }
    public string Data { get; set; }
}
