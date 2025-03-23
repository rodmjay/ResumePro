using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class CountryMapping : Profile
{
    public CountryMapping()
    {
        CreateMap<Country, CountryDto>()
            .IncludeAllDerived();

        CreateMap<Country, CountryDetails>()
            .IncludeAllDerived();

        CreateMap<Country, CountryWithStateProvincesOutput>()
            .ForMember(x => x.StateProvinces, opt => opt.MapFrom(x => x.StateProvinces))
            .IncludeAllDerived();

        CreateMap<Country, DropdownItem>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Iso2));
    }
}