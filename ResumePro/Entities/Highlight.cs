#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Highlight : BaseEntity<Highlight>, IHighlight
{
    public int OrganizationId { get; set; }
    public Job Job { get; set; }
    public int JobId { get; set; }

    public Project Project { get; set; }
    public int? ProjectId { get; set; }

    public int Id { get; set; }
    public int Order { get; set; }
    public string Text { get; set; }

    public override void Configure(EntityTypeBuilder<Highlight> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.Property(x => x.Text)
            .ConfigureColumn(StringColumnSize.Medium);
        
        builder.HasOne(x => x.Job)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new {x.OrganizationId, x.JobId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new {x.ProjectId, x.JobId})
            .HasPrincipalKey(x => new {x.Id, x.JobId})
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}