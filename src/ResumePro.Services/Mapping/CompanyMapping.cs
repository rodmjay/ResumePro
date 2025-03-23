using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class CompanyMapping : Profile
{
    public CompanyMapping()
    {
        CreateMap<Company, CompanyDto>()
            .IncludeAllDerived();

        CreateMap<Company, CompanyDetails>()
            .ForMember(x => x.Positions,
                opt => opt.MapFrom(x => x.Positions.OrderByDescending(a => a.StartDate)));
    }
}