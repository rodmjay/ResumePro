#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Options;

public class PersonLanguageOptions
{
    public string LanguageId { get; set; }
    public LanguageLevel Proficiency { get; set; }
}