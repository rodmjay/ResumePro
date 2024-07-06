using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class CompanyMapping : Profile
{
    public CompanyMapping()
    {
        CreateMap<Company, CompanyDto>().IncludeAllDerived();

        CreateMap<Company, CompanyDetails>()
            .ForMember(x => x.HiringManagers, opt => opt.MapFrom(x => x.HiringManagers));
    }
}