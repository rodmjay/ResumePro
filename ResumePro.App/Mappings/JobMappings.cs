#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class JobMappings : Profile
{
    public JobMappings()
    {
        CreateMap<CompanyDetails, CompanyOptions>()
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Location))
            .ForMember(x => x.Company, opt => opt.MapFrom(x => x.CompanyName))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
            .ForMember(x => x.JobSkillIds, opt => opt.MapFrom(x => x.Skills.Select(s => s.SkillId)))
            .ForMember(x => x.PositionOptions, opt => opt.MapFrom(x => x.Positions))
            .ForMember(x => x.ProjectOptions, opt => opt.MapFrom(x => x.Projects));
    }
}