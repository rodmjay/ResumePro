using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ResumeSkillMapping : Profile
{
    public ResumeSkillMapping()
    {
        CreateMap<ResumeSkill, ResumeSkillDto>()
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Skill.Skill.Title))
            .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Skill.Rating));
    }
}