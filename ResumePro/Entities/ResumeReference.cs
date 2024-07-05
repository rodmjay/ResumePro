#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class ResumeReference : BaseEntity<ResumeReference>
{
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public int PersonaId { get; set; }
    public Resume Resume { get; set; }
    
    public int ReferenceId { get; set; }
    public Reference Reference { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeReference> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.ReferenceId, x.ResumeId});

        builder.HasOne(x => x.Reference)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId, x.ReferenceId})
            .HasPrincipalKey(x=>new{x.OrganizationId, x.PersonaId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.References)
            .HasForeignKey(x => new {x.OrganizationId, x.ResumeId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);
    }
}