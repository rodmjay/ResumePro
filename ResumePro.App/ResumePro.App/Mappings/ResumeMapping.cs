#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<ResumeDetails, ResumeOptions>()
            .ForMember(x => x.JobIds, opt => opt.MapFrom(x => x.Jobs.Select(j => j.Id)))
            .ForMember(x => x.SkillIds, opt => opt.MapFrom(x => x.Skills.Select(j => j.SkillId)))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(x => x.Settings, opt => opt.MapFrom(x => x.Settings))
            .ForMember(x => x.JobTitle, opt => opt.MapFrom(x => x.JobTitle));
    }
}