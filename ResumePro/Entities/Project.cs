﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Project : BaseEntity<Project>, IProject
{
    public int OrganizationId { get; set; }
    public Job Job { get; set; }
    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public int Id { get; set; }
    public int JobId { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? Budget { get; set; }

    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id, x.JobId});

        builder.Property(x => x.Name)
            .ConfigureColumn(StringColumnSize.Small);

        builder.Property(x => x.Description)
            .ConfigureColumn(StringColumnSize.Medium, false);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => new {x.OrganizationId, x.JobId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}