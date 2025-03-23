using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class JobSkillMapping : Profile
{
    public JobSkillMapping()
    {
        CreateMap<CompanySkill, CompanySkillDto>()
            .ForMember(x => x.OrganizationId, opt => opt.Ignore())
            .ForMember(x => x.CompanyId, opt => opt.Ignore())
            .ForMember(x => x.PersonId, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Skill.Skill.Title));
    }
}