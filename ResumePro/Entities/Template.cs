#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Template : BaseEntity<Template>, ITemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Source { get; set; }
    public string Format { get; set; }
    public string Engine { get; set; }
    public int? OrganizationId { get; set; }
    public ICollection<Rendering> Renderings { get; set; } = new List<Rendering>();


    public ICollection<ResumeSettings> Resumes { get; set; }

    public override void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasKey(x => x.Id);
    }
}