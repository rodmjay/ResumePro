#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class HighlightsMappings : Profile
{
    public HighlightsMappings()
    {
        CreateMap<HighlightDto, HighlightOptions>()
            .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Text))
            .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Order))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));
    }
}