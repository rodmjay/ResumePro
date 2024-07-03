#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Data;

namespace ResumePro.Core.Settings;

public class AppSettings
{
    public DatabaseSettings Database { get; set; }
    public string Name { get; set; }
}