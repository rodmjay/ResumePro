#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;

namespace ResumePro.Core.Data;

[ExcludeFromCodeCoverage]
public class DatabaseSettings
{
    public int Timeout { get; set; }
    public string ConnectionStringName { get; set; }
}