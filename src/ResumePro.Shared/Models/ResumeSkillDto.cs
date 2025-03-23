namespace ResumePro.Shared.Models;

public class ResumeSkillDto : IResumeSkill
{
    public string Title { get; set; } = null!;
    public virtual int Rating { get; set; }
    public string[] Categories { get; set; } = [];

    [JsonIgnore] public int ResumeId { get; set; }

    public int SkillId { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }
}