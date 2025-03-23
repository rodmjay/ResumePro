using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Interfaces;

public interface IOrganizationSettings
{
    int OrganizationId { get; set; }
    int ResumeYearHistory { get; set; }
    bool AttachAllJobs { get; set; }
    bool AttachAllSkills { get; set; }
    int DefaultTemplateId { get; set; }
    bool ShowTechnologyPerJob { get; set; }
    bool ShowDuration { get; set; }
    bool ShowContactInfo { get; set; }
    SkillView SkillView { get; set; }
    bool ShowRatings { get; set; }
}