#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class ReferenceMapping : Profile
{
    public ReferenceMapping()
    {
        CreateMap<ReferenceDto, ReferenceOptions>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Text))
            .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Order));
    }
}