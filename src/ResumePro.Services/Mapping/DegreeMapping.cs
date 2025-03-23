using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class DegreeMapping : Profile
{
    public DegreeMapping()
    {
        CreateMap<Degree, DegreeDto>()
            .IncludeAllDerived();
    }
}