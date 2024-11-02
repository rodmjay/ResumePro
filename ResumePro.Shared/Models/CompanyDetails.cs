#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public sealed class CompanyDetails : CompanyDto
{
    private List<CompanySkillDto> _skills;

    [JsonIgnore] public bool ShowTechnology { get; set; } = true;

    [JsonProperty("technology")] public List<CompanySkillDto> Skills { get; set; }

    public List<PositionDetails> Positions { get; set; }

}