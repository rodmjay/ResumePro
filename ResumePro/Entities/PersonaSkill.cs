#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class PersonaSkill : BaseEntity<PersonaSkill>
{
    public int PersonaId { get; set; }
    public Persona Persona { get; set; }
    public int SkillId { get; set; }
    public Skill Skill { get; set; }

    public int Rating { get; set; }

    public ICollection<ResumeSkill> Resumes { get; set; }

    public override void Configure(EntityTypeBuilder<PersonaSkill> builder)
    {
        builder.HasKey(x => new {x.PersonaId, x.SkillId});

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Personas)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}