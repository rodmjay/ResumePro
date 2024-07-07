#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class ResumeJob : BaseEntity<ResumeJob>
{
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeJob> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.ResumeId, x.JobId});

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => new {x.OrganizationId, x.ResumeId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x =>  new{x.OrganizationId, x.JobId})
            .HasPrincipalKey(x=>new{x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}