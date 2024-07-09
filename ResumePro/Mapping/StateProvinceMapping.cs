#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;

namespace ResumePro.Mapping;

public class StateProvinceMapping : Profile
{
    public StateProvinceMapping()
    {
        CreateMap<StateProvince, StateProvinceOutput>()
            .IncludeAllDerived();

        CreateMap<StateProvince, DropdownItem>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Code));
    }
}