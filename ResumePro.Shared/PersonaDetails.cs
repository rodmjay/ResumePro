﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class PersonaDetails : PersonaDto
{
    public List<ResumeDto> Resumes { get; set; }
    public List<PersonaSkillDto> Skills { get; set; }
    public List<JobDetails> Jobs { get; set; }
}