#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class Degree : BaseEntity<Degree>, IDegree
{
    public School School { get; set; }
    public int SchoolId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }

    public override void Configure(EntityTypeBuilder<Degree> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.School)
            .WithMany(x => x.Degrees)
            .HasForeignKey(x => x.SchoolId);
    }
}