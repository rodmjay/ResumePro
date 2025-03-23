using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class StateProvince : BaseEntity<StateProvince>, IStateProvince
{
    public int Id { get; set; }
    public Country Country { get; set; } = null!;
    public string Iso2 { get; set; } = null!;

    public ICollection<Persona> People { get; set; } = new List<Persona>();

    public string Name { get; set; } = null!;
    public string Abbrev { get; set; } = null!;
    public string Code { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<StateProvince> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Country)
            .WithMany(x => x.StateProvinces)
            .HasForeignKey(x => x.Iso2);
    }
}