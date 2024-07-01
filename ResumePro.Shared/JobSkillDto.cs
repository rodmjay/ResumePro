#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class JobSkillDto : ResumeSkillDto
{
    [JsonIgnore] public override int Rating { get; set; }
}