#region Header Info
// Copyright 2024 Rod Johnson.  All rights reserved
#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class JobMappings : Profile
{
    public JobMappings()
    {
        CreateMap<JobDetails, JobOptions>()
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Location))
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
            .ForMember(x => x.Company, opt => opt.MapFrom(x => x.Company))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
            .ForMember(x => x.HighlightOptions, opt => opt.MapFrom(x => x.Highlights))
            .ForMember(x => x.ProjectOptions, opt => opt.MapFrom(x => x.Projects));
    }
}