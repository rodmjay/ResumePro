#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class OrganizationSettings : BaseEntity<OrganizationSettings>
{
    public int OrganizationId { get; set; }

    public int ResumeYearHistory { get; set; }

    public int DefaultTemplate { get; set; }

    public override void Configure(EntityTypeBuilder<OrganizationSettings> builder)
    {
        builder.HasKey(x => x.OrganizationId);
    }
}