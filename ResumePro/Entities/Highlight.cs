#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class Highlight : BaseEntity<Highlight>, IHighlight
{
    public Job Job { get; set; }
    public int JobId { get; set; }

    public Project Project { get; set; }
    public int? ProjectId { get; set; }

    public int Id { get; set; }
    public int Order { get; set; }
    public string Text { get; set; }
    public override void Configure(EntityTypeBuilder<Highlight> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Highlighs)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new { x.ProjectId, x.JobId })
            .HasPrincipalKey(x => new { x.Id, x.JobId })
            .IsRequired(false);
    }
}