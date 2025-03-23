using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class ResumeSettingsMapping : Profile
{
    public ResumeSettingsMapping()
    {
        CreateMap<ResumeSettings, ResumeSettingsDto>()
            .ForMember(x => x.ResumeYearHistory,
                opt => opt.MapFrom(x => x.ResumeYearHistory ?? x.OrganizationSettings.ResumeYearHistory))
            .ForMember(x => x.ShowTechnologyPerJob,
                opt => opt.MapFrom(x => x.ShowTechnologyPerJob ?? x.OrganizationSettings.ShowTechnologyPerJob))
            .ForMember(x => x.AttachAllJobs,
                opt => opt.MapFrom(x => x.AttachAllJobs ?? x.OrganizationSettings.AttachAllJobs))
            .ForMember(x => x.AttachAllSkills,
                opt => opt.MapFrom(x => x.AttachAllSkills ?? x.OrganizationSettings.AttachAllSkills))
            .ForMember(x => x.DefaultTemplateId,
                opt => opt.MapFrom(x => x.DefaultTemplateId ?? x.OrganizationSettings.DefaultTemplateId))
            .ForMember(x => x.ShowDuration,
                opt => opt.MapFrom(x => x.ShowDuration ?? x.OrganizationSettings.ShowDuration))
            .ForMember(x => x.ShowContactInfo,
                opt => opt.MapFrom(x => x.ShowContactInfo ?? x.OrganizationSettings.ShowContactInfo))
            .ForMember(x => x.SkillView,
                opt => opt.MapFrom(x => x.SkillView ?? x.OrganizationSettings.SkillView))
            .ForMember(x => x.ShowRatings,
                opt => opt.MapFrom(x => x.ShowRatings ?? x.OrganizationSettings.ShowRatings));
    }
}