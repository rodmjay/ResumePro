using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping
{
    public class PersonaMapping : Profile
    {
        public PersonaMapping()
        {
            CreateMap<Persona, PersonaDto>()
                .IncludeAllDerived();
        }

    }
}
