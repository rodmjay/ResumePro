#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Enums;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class OrganizationSettings : BaseEntity<OrganizationSettings>, IOrganizationSettings
{
    public ICollection<ResumeSettings> ResumeSettings { get; set; }
    public int OrganizationId { get; set; }
    public int ResumeYearHistory { get; set; }
    public int DefaultTemplate { get; set; }
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
    public string DefaultTemplateId { get; set; }
    public bool ShowTechnologyPerJob { get; set; }
    public bool ShowDuration { get; set; }
    public bool ShowContactInfo { get; set; }
    public SkillView SkillView { get; set; }
    public bool ShowRatings { get; set; }

    public override void Configure(EntityTypeBuilder<OrganizationSettings> builder)
    {
        builder.HasKey(x => x.OrganizationId);
    }
}