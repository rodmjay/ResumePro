#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class ResumeSettings : BaseEntity<ResumeSettings>, IResumeSettings
{
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
    public int ResumeYearHistory { get; set; }
    public string DefaultTemplateId { get; set; }
    public bool ShowTechnologyPerJob { get; set; }
    public bool ShowDuration { get; set; }
    public bool ShowRatings { get; set; }
    public Template Template { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeSettings> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.ResumeId });

        builder.HasOne(x => x.Resume)
            .WithOne(x => x.ResumeSettings)
            .HasForeignKey<ResumeSettings>(x => new { x.OrganizationId, x.ResumeId })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Template)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => x.DefaultTemplateId)
            .HasPrincipalKey(x=>x.Name)
            .IsRequired(false);
    }
}