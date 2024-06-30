#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class ResumeDetails : ResumeDto
{
    public List<JobDetails> Jobs { get; set; }
    public List<ResumeSkillDto> Skills { get; set; }
    public List<ReferenceDto> References { get; set; }
}