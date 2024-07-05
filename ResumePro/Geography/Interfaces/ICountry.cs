#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion


namespace ResumePro.Geography.Interfaces
{
    public interface ICountry
    {
        string Name { get; set; }
        string Iso2 { get; set; }
        string CapsName { get; set; }
        string Iso3 { get; set; }
        int? NumberCode { get; set; }
        int PhoneCode { get; set; }
    }
}