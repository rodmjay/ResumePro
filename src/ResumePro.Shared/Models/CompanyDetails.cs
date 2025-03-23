namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public sealed class CompanyDetails : CompanyDto
{
    [JsonIgnore] public bool ShowTechnology { get; set; } = true;

    [JsonProperty("technology")] public List<CompanySkillDto> Skills { get; set; } = [];

    public List<PositionDetails> Positions { get; set; } = [];
}