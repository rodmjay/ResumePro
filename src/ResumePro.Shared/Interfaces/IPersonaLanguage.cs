using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Interfaces;

public interface IPersonaLanguage
{
    int OrganizationId { get; set; }
    int PersonId { get; set; }
    string Code3 { get; set; }
    LanguageLevel Proficiency { get; set; }
}