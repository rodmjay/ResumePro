#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public sealed class Template : BaseEntity<Template>
{
    public string Name { get; set; }
    public string Source { get; set; }
    public string Format { get; set; }

    public ICollection<ResumeSettings> Resumes { get; set; }

    public override void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasKey(x => x.Name);
    }
}