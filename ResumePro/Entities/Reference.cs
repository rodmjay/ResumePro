#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class Reference : BaseEntity<Reference>, IReference
{
    public int OrganizationId { get; set; }
    public Job Job { get; set; }
    public int JobId { get; set; }
    public int Id { get; set; }
    public string Text { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    public override void Configure(EntityTypeBuilder<Reference> builder)
    {

        builder.HasKey(x => new{x.OrganizationId, x.Id});

        builder.HasOne(x => x.Job)
            .WithMany(x => x.References)
            .HasForeignKey(x => new{x.OrganizationId, x.JobId})
            .HasPrincipalKey(x=>new{x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}