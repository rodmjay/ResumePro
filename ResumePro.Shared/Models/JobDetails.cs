#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class JobDetails : JobDto
{
    private List<JobSkillDto> _skills;

    [JsonIgnore] public bool ShowTechnology { get; set; } = true;

    public List<HighlightDto> Highlights { get; set; }

    [JsonProperty("technology")]
    public List<JobSkillDto> Skills
    {
        get => !ShowTechnology ? null : _skills;
        set => _skills = value;
    }

    public List<ProjectDetails> Projects { get; set; }
}