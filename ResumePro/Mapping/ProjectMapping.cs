#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<Project, ProjectDto>()
            .IncludeAllDerived();

        CreateMap<Project, ProjectDetails>()
            .ForMember(x=>x.Highlights, opt=>opt.MapFrom(x=>x.Highlights));
    }
}