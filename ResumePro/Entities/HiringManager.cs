#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class HiringManager : BaseEntity<HiringManager>, IHiringManager
{
    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<JobPosting> JobPostings { get; set; }
    public override void Configure(EntityTypeBuilder<HiringManager> builder)
    {
        builder.HasKey(x => new{x.OrganizationId, x.Id});

        builder.HasOne(x => x.Company)
            .WithMany(x => x.HiringManagers)
            .HasForeignKey(x => new {x.OrganizationId, x.CompanyId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);
    }
}