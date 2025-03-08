#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class RenderingMapping : Profile
{
    public RenderingMapping()
    {
        CreateMap<Rendering, RenderingDto>()
            .ForMember(x => x.Engine, opt => opt.MapFrom(x => x.Template.Engine))
            .ForMember(x => x.Format, opt => opt.MapFrom(x => x.Template.Format))
            .IncludeAllDerived();
    }
}