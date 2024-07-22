#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class SchoolMapping : Profile
{
    public SchoolMapping()
    {
        CreateMap<SchoolDetails, SchoolOptions>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Degrees, opt => opt.MapFrom(x => x.Degrees));
    }
}