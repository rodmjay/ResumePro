#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Mapping;

public class JobMapping : Profile
{
    public JobMapping()
    {
        CreateMap<Job, JobDto>()
            .IncludeAllDerived();

        CreateMap<Job, JobDetails>()
            .ForMember(x => x.Highlights,
                opt => opt.MapFrom(x => x.Highlights.OrderBy(a => a.Order).Where(h => h.ProjectId == null)))
            .ForMember(x => x.Projects, opt => opt.MapFrom(x => x.Projects.OrderBy(a => a.Order)));
    }
}