﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Mapping;

public class CompanyMapping : Profile
{
    public CompanyMapping()
    {
        CreateMap<Company, CompanyDto>()
            .IncludeAllDerived();

        CreateMap<Company, CompanyDetails>()
            .ForMember(x => x.Positions,
                opt => opt.MapFrom(x => x.Positions.OrderByDescending(a => a.StartDate)));
    }
}