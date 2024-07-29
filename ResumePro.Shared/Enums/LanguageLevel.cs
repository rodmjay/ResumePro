#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel;

namespace ResumePro.Shared.Enums;

public enum LanguageLevel : int
{
    [Description("None")]
    None = 0,

    [Description("1-Beginner")]
    Beginner = 1,

    [Description("2-Elementary")]
    Elementary = 2,

    [Description("3-Intermediate")]
    Intermediate = 3,

    [Description("4-Advanced")]
    Advanced = 4,

    [Description("5-Fluent")]
    Fluent = 5

}