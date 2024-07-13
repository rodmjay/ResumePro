#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;

namespace ResumePro.Mapping;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(x => x.JobId, opt => opt.Ignore())
            .IncludeAllDerived();

        CreateMap<Project, ProjectDetails>()
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Highlights.OrderBy(a => a.Order)));
    }
}