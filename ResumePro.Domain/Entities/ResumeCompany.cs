#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class ResumeCompany : BaseEntity<ResumeCompany>
{
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeCompany> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.ResumeId, x.CompanyId});

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => new {x.OrganizationId, x.ResumeId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new {x.OrganizationId, x.CompanyId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}