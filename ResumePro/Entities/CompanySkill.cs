﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class CompanySkill : BaseEntity<CompanySkill>, ICompanySkill
{
    public Company Company { get; set; }
    public PersonaSkill Skill { get; set; }
    public int OrganizationId { get; set; }
    public int CompanyId { get; set; }
    public int PersonId { get; set; }
    public int SkillId { get; set; }

    public override void Configure(EntityTypeBuilder<CompanySkill> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.PersonId, x.CompanyId, x.SkillId});

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new {x.OrganizationId, x.CompanyId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonId, x.SkillId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.PersonId, x.SkillId})
            .OnDelete(DeleteBehavior.Cascade);
    }
}