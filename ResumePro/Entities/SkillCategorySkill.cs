﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class SkillCategorySkill : BaseEntity<SkillCategorySkill>
{
    public int SkillId { get; set; }
    public Skill Skill { get; set; }

    public int SkillCategoryId { get; set; }
    public SkillCategory SkillCategory { get; set; }

    public override void Configure(EntityTypeBuilder<SkillCategorySkill> builder)
    {
        builder.HasKey(x => new {x.SkillCategoryId, x.SkillId});

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.SkillId);

        builder.HasOne(x => x.SkillCategory)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.SkillCategoryId);
    }
}