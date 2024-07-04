#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class JobSkill : BaseEntity<JobSkill>
{
    public int OrganizationId { get; set; }
    public int JobId { get; set; }
    public int PersonaId { get; set; }
    public Job Job { get; set; }
    public PersonaSkill Skill { get; set; }
    public int SkillId { get; set; }

    public override void Configure(EntityTypeBuilder<JobSkill> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.SkillId, x.JobId});

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new {x.OrganizationId, x.JobId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId, x.SkillId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.PersonaId, x.SkillId})
            .OnDelete(DeleteBehavior.NoAction);
    }
}