﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class SkillCategory : BaseEntity<SkillCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<SkillCategorySkill> Skills { get; set; }

    public override void Configure(EntityTypeBuilder<SkillCategory> builder)
    {
        builder.HasKey(x => x.Id);
    }
}