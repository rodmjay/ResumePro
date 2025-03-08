#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class CountryDto : ICountry
{
    public string Name { get; set; }

    public string Iso2 { get; set; }

    public string CapsName { get; set; }

    public string Iso3 { get; set; }

    public int? NumberCode { get; set; }

    public int PhoneCode { get; set; }
}