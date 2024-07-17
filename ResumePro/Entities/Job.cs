#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Job : BaseEntity<Job>, IJob
{
    public int OrganizationId { get; set; }
    public Persona Persona { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public int PersonaId { get; set; }
    public ICollection<ResumeJob> Resumes { get; set; } = new List<ResumeJob>();
    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public ICollection<JobSkill> Skills { get; set; } = new List<JobSkill>();
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }

    public override void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);
    }
}