using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Models;

public class PersonaLanguageDto : IPersonaLanguage
{
    public string LanguageName { get; set; } = null!;

    [JsonIgnore] public int OrganizationId { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public string Code3 { get; set; } = null!;
    public LanguageLevel Proficiency { get; set; }
}