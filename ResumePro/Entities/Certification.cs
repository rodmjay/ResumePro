﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class Certification : BaseEntity<Certification>
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Body { get; set; }
    public DateTime Date { get; set; }
    public Persona Persona { get; set; }
    public int PersonaId { get; set; }
    public int Id { get; set; }
    public override void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Certifications)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);
    }
}