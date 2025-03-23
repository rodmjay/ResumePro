using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Options;

public class OrganizationSettingsOptions
{
    public int ResumeYearHistory { get; set; } = 10;
    public int DefaultTemplateId { get; set; } = 2;
    public bool ShowTechnologyPerJob { get; set; } = false;
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
    public bool ShowRatings { get; set; } = false;
    public bool ShowDuration { get; set; } = true;
    public bool ShowContactInfo { get; set; } = false;
    public SkillView SkillView { get; set; } = SkillView.Grouped;
}