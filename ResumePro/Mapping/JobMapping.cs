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
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Highlighs));

        CreateMap<ResumeJob, JobDetails>()
            .ForMember(x => x.Company, opt => opt.MapFrom(x => x.Job.Company))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Job.Description))
            .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Job.Location))
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Job.Title))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.Job.StartDate))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.Job.EndDate))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Job.Skills
                .OrderByDescending(a => a.Skill.Rating)))
            .ForMember(x => x.Projects, opt => opt.MapFrom(x => x.Job.Projects.OrderBy(a => a.Order)))
            .ForMember(x => x.Highlights,
                opt => opt.MapFrom(x => x.Job.Highlighs.OrderBy(a => a.Order).Where(a => a.ProjectId == null)));
    }
}