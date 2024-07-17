#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class CountryWithStateProvincesOutput : CountryDetails
{
    public CountryWithStateProvincesOutput()
    {
        StateProvinces = new List<StateProvinceOutput>();
    }

    public List<StateProvinceOutput> StateProvinces { get; set; }
}