#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class ResumeSettingsMapping : Profile
{
    public ResumeSettingsMapping()
    {
        CreateMap<ResumeSettingsDto, ResumeSettingsOptions>()
            .ForMember(x => x.DefaultTemplateId, opt => opt.MapFrom(x => x.DefaultTemplateId))
            .ForMember(x => x.ResumeYearHistory, opt => opt.MapFrom(x => x.ResumeYearHistory))
            .ForMember(x => x.ShowTechnologyPerJob, opt => opt.MapFrom(x => x.ShowTechnologyPerJob))
            .ForMember(x => x.AttachAllJobs, opt => opt.MapFrom(x => x.AttachAllJobs))
            .ForMember(x => x.AttachAllSkills, opt => opt.MapFrom(x => x.AttachAllSkills))
            .ForMember(x => x.ShowRatings, opt => opt.MapFrom(x => x.ShowRatings))
            .ForMember(x => x.ShowDuration, opt => opt.MapFrom(x => x.ShowDuration))
            .ForMember(x => x.ShowContactInfo, opt => opt.MapFrom(x => x.ShowContactInfo))
            .ForMember(x => x.SkillView, opt => opt.MapFrom(x => x.SkillView));
    }
}