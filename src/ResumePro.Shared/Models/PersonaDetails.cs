namespace ResumePro.Shared.Models;

public class PersonaDetails : PersonaDto
{
    public List<ResumeDto> Resumes { get; set; } = new();
    public List<PersonaSkillDto> Skills { get; set; } = new();
    public List<CompanyDetails> Jobs { get; set; } = new();
    public List<SchoolDetails> Education { get; set; } = new();
    public List<PersonaLanguageDto> Languages { get; set; } = new();
    public List<CertificationDto> Certifications { get; set; } = new();
    public List<ReferenceDto> References { get; set; } = new();
}