using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping
{
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
}
