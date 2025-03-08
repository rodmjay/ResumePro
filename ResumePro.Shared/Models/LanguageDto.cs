#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class LanguageDto : ILanguage
{
    public string Name { get; set; }
    public string Code2 { get; set; }
    public string Code3 { get; set; }
}