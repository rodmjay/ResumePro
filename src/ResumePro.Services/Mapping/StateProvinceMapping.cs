using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public sealed class StateProvinceMapping : Profile
{
    public StateProvinceMapping()
    {
        CreateMap<StateProvince, StateProvinceOutput>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .IncludeAllDerived();

        CreateMap<StateProvince, DropdownItem>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Id));
    }
}