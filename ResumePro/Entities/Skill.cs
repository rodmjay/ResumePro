#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class Skill : BaseEntity<Skill>, ISkill
{
    public ICollection<PersonaSkill> Personas { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }

    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);
    }
}