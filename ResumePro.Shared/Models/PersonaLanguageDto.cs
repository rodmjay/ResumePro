#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Models;

public class PersonaLanguageDto : IPersonaLanguage
{
    public string LanguageName { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public string Code3 { get; set; }
    public LanguageLevel Proficiency { get; set; }
}