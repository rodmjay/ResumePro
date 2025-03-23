using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Country : BaseEntity<Country>, ICountry
{
    public ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
    public string Iso2 { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string CapsName { get; set; } = null!;
    public string Iso3 { get; set; } = null!;
    public int? NumberCode { get; set; }
    public int PhoneCode { get; set; }

    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Iso2);

        builder.Property(x => x.Iso2)
            .HasMaxLength(2);

        builder.Property(x => x.Iso3)
            .HasMaxLength(3);

        builder.Property(x => x.Name)
            .HasMaxLength(80)
            .IsRequired();

        builder.HasIndex(x => x.Iso2);

        builder.HasIndex(x => x.Iso3);

        builder.HasMany(x => x.StateProvinces)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.Iso2);
    }
}