#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class Degree : BaseEntity<Degree>, IDegree
{
    public int OrganizationId { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }

    public override void Configure(EntityTypeBuilder<Degree> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.HasOne(x => x.School)
            .WithMany(x => x.Degrees)
            .HasForeignKey(x => new {x.OrganizationId, x.SchoolId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}