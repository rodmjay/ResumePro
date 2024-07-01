#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class School : BaseEntity<School>, ISchool
{
    public Persona Persona { get; set; }

    public ICollection<Degree> Degrees { get; set; }
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name { get; set; }

    public override void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Schools)
            .HasForeignKey(x => x.PersonaId);
    }
}