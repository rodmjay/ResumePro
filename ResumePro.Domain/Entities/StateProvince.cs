#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class StateProvince : BaseEntity<StateProvince>, IStateProvince
{
    public int Id { get; set; }
    public Country Country { get; set; }
    public string Iso2 { get; set; }

    public ICollection<Persona> People { get; set; } = new List<Persona>();

    public string Name { get; set; }
    public string Abbrev { get; set; }
    public string Code { get; set; }

    public override void Configure(EntityTypeBuilder<StateProvince> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Country)
            .WithMany(x => x.StateProvinces)
            .HasForeignKey(x => x.Iso2);
    }
}