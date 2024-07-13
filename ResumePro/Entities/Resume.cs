#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class Resume : BaseEntity<Resume>, IResume
{
    public Persona Persona { get; set; }
    public ICollection<ResumeJob> Jobs { get; set; } = new List<ResumeJob>();
    public ICollection<ResumeSkill> Skills { get; set; } = new List<ResumeSkill>();
    public ResumeSettings ResumeSettings { get; set; }
    public int OrganizationId { get; set; }
    public int PersonaId { get; set; }
    public int Id { get; set; }
    public string JobTitle { get; set; }
    public string Description { get; set; }

    public override void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}