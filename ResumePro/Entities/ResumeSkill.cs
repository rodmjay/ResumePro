#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class ResumeSkill : BaseEntity<ResumeSkill>, IResumeSkill
{
    public Resume Resume { get; set; }
    public PersonaSkill Skill { get; set; }
    public ICollection<JobSkill> Jobs { get; set; }
    public bool ShowInSummary { get; set; }
    public int ResumeId { get; set; }
    public int SkillId { get; set; }
    public int PersonaId { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeSkill> builder)
    {
        builder.HasKey(x => new {x.PersonaId, x.ResumeId, x.SkillId});

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new {x.PersonaId, x.ResumeId})
            .HasPrincipalKey(x => new {x.PersonaId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new {x.PersonaId, x.SkillId})
            .HasPrincipalKey(x => new {x.PersonaId, x.SkillId})
            .OnDelete(DeleteBehavior.NoAction);
    }
}