#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<ResumeDetails, ResumeOptions>()
            .ForMember(x => x.JobIds, opt => opt.MapFrom(x => x.Jobs.Select(j => j.Id)))
            .ForMember(x => x.SkillIds, opt => opt.MapFrom(x => x.Skills.Select(j => j.SkillId)))
            .ForMember(x => x.Settings, opt => opt.MapFrom(x => x.Settings));
    }
}