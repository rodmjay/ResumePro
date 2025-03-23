using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class PersonaLanguageMapping : Profile
{
    public PersonaLanguageMapping()
    {
        CreateMap<PersonaLanguage, PersonaLanguageDto>()
            .ForMember(x => x.LanguageName, opt => opt.MapFrom(x => x.Language.Name))
            .ForMember(x => x.Code3, opt => opt.MapFrom(x => x.Code3))
            .ForMember(x => x.Proficiency, opt => opt.MapFrom(x => x.Proficiency));
    }
}