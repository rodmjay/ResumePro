#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.AI.Entities;

public class ApiKey : BaseEntity<ApiKey>
{
    public int OrganizationId { get; set; }
    public int UserId { get; set; }
    public string Key { get; set; }

    public override void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.UserId});
    }
}