#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class PersonaSkill : BaseEntity<PersonaSkill>, IPersonaSkill
{
    public int OrganizationId { get; set; }
    public Persona Persona { get; set; }
    public Skill Skill { get; set; }
    public ICollection<JobSkill> Jobs { get; set; }

    public ICollection<ResumeSkill> Resumes { get; set; }
    public int PersonaId { get; set; }
    public int SkillId { get; set; }
    public int Rating { get; set; }

    public override void Configure(EntityTypeBuilder<PersonaSkill> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.PersonaId, x.SkillId});

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Personas)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);
    }
}