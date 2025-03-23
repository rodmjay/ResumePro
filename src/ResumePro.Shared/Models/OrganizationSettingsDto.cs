using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Models;

public class OrganizationSettingsDto : IOrganizationSettings
{
    public int OrganizationId { get; set; }
    public int ResumeYearHistory { get; set; }
    public bool AttachAllJobs { get; set; }
    public bool AttachAllSkills { get; set; }
    public int DefaultTemplateId { get; set; }
    public bool ShowTechnologyPerJob { get; set; }
    public bool ShowDuration { get; set; }
    public bool ShowContactInfo { get; set; }
    public SkillView SkillView { get; set; }
    public bool ShowRatings { get; set; }
}