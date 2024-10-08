﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class SchoolMapping : Profile
{
    public SchoolMapping()
    {
        CreateMap<SchoolDetails, SchoolOptions>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
            .ForMember(x => x.DegreeOptions, opt => opt.MapFrom(x => x.Degrees));
    }
}