#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Data;

namespace ResumePro.Core.Settings;

public class AppSettings
{
    public string ApiUrl { get; set; }
    public string AppUrl { get; set; }
    public string ResumeApiUrl { get; set; }
    public string Authority { get; set; }
    public string Audience { get; set; }
    public DatabaseSettings Database { get; set; }
    public string Name { get; set; }
    public string CodeSigningThumbprint { get; set; }
}