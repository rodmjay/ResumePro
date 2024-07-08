#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Extensions;

namespace ResumePro.Shared;

public class ResumeDetails : ResumeDto
{
    public List<JobDetails> Jobs { get; set; }
    public List<ResumeSkillDto> Skills { get; set; }
    public List<ReferenceDto> References { get; set; }
    public List<SchoolDetails> Education { get; set; }
    public List<PersonaLanguageDto> Languages { get; set; }
    public List<CertificationDto> Certifications { get; set; }

    public string LanguageString => this.GetLanguageString();
}