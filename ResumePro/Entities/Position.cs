﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Position : BaseEntity<Position>, IPosition
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public int PersonId { get; set; }
    public int CompanyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Company Company { get; set; }
    public string JobTitle { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();

    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public override void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.Id });

        builder.Property(x => x.JobTitle)
            .ConfigureColumn(StringColumnSize.Small);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Positions)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.Id });
    }

}