#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class SchoolMapping : Profile
{
    public SchoolMapping()
    {
        CreateMap<School, SchoolDto>().IncludeAllDerived();

        CreateMap<School, SchoolDetails>()
            .ForMember(x => x.Degrees, opt => opt.MapFrom(x => x.Degrees.OrderBy(d => d.Order)));
    }
}