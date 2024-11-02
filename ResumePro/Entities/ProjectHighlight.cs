#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public sealed class ProjectHighlight : BaseEntity<ProjectHighlight>
{
    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Project Project { get; set; }
    public int ProjectId { get; set; }
    public int PersonId { get; set; }
    public int Order { get; set; }
    public string Text { get; set; }
    public override void Configure(EntityTypeBuilder<ProjectHighlight> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.PersonId, x.CompanyId, x.ProjectId, x.Id});

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonId, x.CompanyId, x.ProjectId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.PersonId, x.CompanyId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}