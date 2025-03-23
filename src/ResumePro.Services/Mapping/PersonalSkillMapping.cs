using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class PersonalSkillMapping : Profile
{
    public PersonalSkillMapping()
    {
        CreateMap<PersonaSkill, PersonaSkillDto>()
            .ForMember(x => x.Categories, opt => opt.MapFrom(x => x.Skill.Categories.Select(a => a.SkillCategory.Name)))
            .ForMember(x => x.PersonId, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Skill.Title));
    }
}