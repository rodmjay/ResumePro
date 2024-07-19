#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Models;

public class PersonaLanguageDto : IPersonaLanguage
{
    [JsonIgnore]
    public int OrganizationId { get; set; }

    [JsonIgnore]
    public int PersonaId { get; set; }
    public string Code3 { get; set; }
    public string LanguageName { get; set; }
    public int Proficiency { get; set; }
}