﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Mapping;

public class PositionMapping : Profile
{
    public PositionMapping()
    {
        CreateMap<Position, PositionDto>()
            .IncludeAllDerived();

        CreateMap<Position, PositionDetails>()
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Highlights.OrderBy(a => a.Order)))
            .ForMember(x => x.Projects, opt => opt.MapFrom(x => x.Projects.OrderBy(a => a.Order)));
    }
}