using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class SchoolMapping : Profile
{
    public SchoolMapping()
    {
        CreateMap<School, SchoolDto>().IncludeAllDerived();

        CreateMap<School, SchoolDetails>()
            .ForMember(x=>x.Degrees, opt=>opt.MapFrom(x=>x.Degrees));
    }
}