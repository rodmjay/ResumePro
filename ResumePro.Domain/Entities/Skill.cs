#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Skill : BaseEntity<Skill>, ISkill
{
    public ICollection<PersonaSkill> Personas { get; set; }
    public ICollection<SkillCategorySkill> Categories { get; set; }

    public int Id { get; set; }
    public string Title { get; set; }

    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);
    }
}