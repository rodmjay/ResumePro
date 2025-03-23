using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class SkillMapping : Profile
{
    public SkillMapping()
    {
        CreateMap<Skill, SkillDto>()
            .ForMember(x => x.Categories, opt => opt.MapFrom(x => x.Categories.Select(a => a.SkillCategory.Name)))
            .IncludeAllDerived();

        CreateMap<Skill, DropdownItem>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title))
            .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Id));
    }
}