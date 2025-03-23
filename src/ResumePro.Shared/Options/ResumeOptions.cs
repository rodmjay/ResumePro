using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ResumeOptions : IDescription, IJobTitle
{
    public ResumeSettingsOptions Settings { get; set; } = new();

    public List<int> SkillIds { get; set; } = new();

    public List<int> CompanyIds { get; set; } = new();

    [Required] public string Description { get; set; } = null!;
    [Required] public string JobTitle { get; set; } = null!;
}