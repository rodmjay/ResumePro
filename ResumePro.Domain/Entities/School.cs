#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class School : BaseEntity<School>, ISchool
{
    public int OrganizationId { get; set; }
    public Persona Persona { get; set; }
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
    public int Id { get; set; }
    public int PersonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public override void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Schools)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}