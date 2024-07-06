#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class Company : BaseEntity<Company>, ICompany
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Headquarters { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
    public ICollection<HiringManager> HiringManagers { get; set; }
    public ICollection<JobPosting> JobPostings { get; set; }
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.Id });
    }
}