﻿#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class Job : BaseEntity<Job>, IJob
{
    public Persona Persona { get; set; }
    public ICollection<Project> Projects { get; set; }
    public int PersonaId { get; set; }
    public ICollection<ResumeJob> Resumes { get; set; }

    public ICollection<Highlight> Highlighs { get; set; } = new List<Highlight>();
    public ICollection<Reference> References { get; set; } = new List<Reference>();
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }

    public override void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}