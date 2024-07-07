#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class JobMapping : Profile
{
    public JobMapping()
    {
        CreateMap<Job, JobDto>()
            .IncludeAllDerived();

        CreateMap<Job, JobDetails>()
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Highlights));
    }
}