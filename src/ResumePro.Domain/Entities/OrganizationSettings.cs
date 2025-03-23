using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Shared.Enums;

namespace ResumePro.Domain.Entities;

public sealed class OrganizationSettings : BaseEntity<OrganizationSettings>, IOrganizationSettings
{
    public ICollection<ResumeSettings> ResumeSettings { get; set; } = null!;
    public int OrganizationId { get; set; }
    public int ResumeYearHistory { get; set; } = 10;
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
    public int DefaultTemplateId { get; set; } = 2;
    public bool ShowTechnologyPerJob { get; set; }
    public bool ShowDuration { get; set; } = true;
    public bool ShowContactInfo { get; set; } = true;
    public SkillView SkillView { get; set; } = SkillView.Grouped;
    public bool ShowRatings { get; set; }

    public override void Configure(EntityTypeBuilder<OrganizationSettings> builder)
    {
        builder.HasKey(x => x.OrganizationId);
    }
}