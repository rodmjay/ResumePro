using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Options;

public class PersonLanguageOptions
{
    public string LanguageId { get; set; } = null!;
    public LanguageLevel Proficiency { get; set; }
}