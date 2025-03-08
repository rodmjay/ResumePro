#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class PersonaSkill : BaseEntity<PersonaSkill>, IPersonaSkill
{
    public int OrganizationId { get; set; }
    public Persona Persona { get; set; }
    public Skill Skill { get; set; }
    public ICollection<CompanySkill> Jobs { get; set; }
    public ICollection<ResumeSkill> Resumes { get; set; }
    public int PersonId { get; set; }
    public int SkillId { get; set; }

    public override void Configure(EntityTypeBuilder<PersonaSkill> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.PersonId, x.SkillId});

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Personas)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);
    }
}