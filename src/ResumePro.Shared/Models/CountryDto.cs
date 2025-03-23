namespace ResumePro.Shared.Models;

public class CountryDto : ICountry
{
    public string Name { get; set; } = null!;

    public string Iso2 { get; set; } = null!;

    public string CapsName { get; set; } = null!;

    public string Iso3 { get; set; } = null!;

    public int? NumberCode { get; set; }

    public int PhoneCode { get; set; }
}