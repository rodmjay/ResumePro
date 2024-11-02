#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class PersonaDetails : PersonaDto
{
    public List<ResumeDto> Resumes { get; set; }
    public List<PersonaSkillDto> Skills { get; set; }
    public List<CompanyDetails> Jobs { get; set; }
    public List<SchoolDetails> Education { get; set; }
    public List<PersonaLanguageDto> Languages { get; set; }
    public List<CertificationDto> Certifications { get; set; }
    public List<ReferenceDto> References { get; set; }
}