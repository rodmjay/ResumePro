namespace ResumePro.Shared.Interfaces;

public interface ICountry
{
    string Name { get; set; }
    string Iso2 { get; set; }
    string CapsName { get; set; }
    string Iso3 { get; set; }
    int? NumberCode { get; set; }
    int PhoneCode { get; set; }
}