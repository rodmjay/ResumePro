#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Geography.Entities;
using ResumePro.Geography.Models;
using ResumePro.Shared.Common;

namespace ResumePro.Geography.Projections;

public class CountryProjections : Profile
{
    public CountryProjections()
    {
        CreateMap<Country, CountryOutput>()
            .IncludeAllDerived();

        CreateMap<Country, CountryDetails>()
            .IncludeAllDerived();

        CreateMap<Country, CountryWithStateProvincesOutput>()
            .ForMember(x => x.StateProvinces, opt => opt.MapFrom(x => x.StateProvinces))
            .IncludeAllDerived();

        CreateMap<Country, DropdownItem>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Iso2));
    }
}