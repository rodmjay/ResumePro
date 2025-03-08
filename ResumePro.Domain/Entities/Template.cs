#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Template : BaseEntity<Template>, ITemplate
{
    public int? OrganizationId { get; set; }
    public ICollection<Rendering> Renderings { get; set; } = new List<Rendering>();
    public ICollection<ResumeSettings> Resumes { get; set; } = new List<ResumeSettings>();
    public int Id { get; set; }
    public string Name { get; set; }
    public string Source { get; set; }
    public string Format { get; set; }
    public string Engine { get; set; }

    public override void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Source)
            .HasColumnType("TEXT");

        builder.Property(x => x.Format)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.Engine)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.Name)
            .ConfigureColumn(StringColumnSize.VerySmall);
    }
}