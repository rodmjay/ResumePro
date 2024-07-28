#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class ProjectMappings : Profile
{
    public ProjectMappings()
    {
        CreateMap<ProjectDetails, ProjectOptions>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Order))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Budget, opt => opt.MapFrom(x => x.Budget))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(x => x.HighlightOptions, opt => opt.MapFrom(x => x.Highlights));
    }
}