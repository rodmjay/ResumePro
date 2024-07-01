#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class JobDetails : JobDto
{
    public List<HighlightDto> Highlights { get; set; }

    [JsonProperty("technology")]
    public List<JobSkillDto> Skills { get; set; }
    public List<ProjectDetails> Projects { get; set; }
}