using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class ReferenceMapping : Profile
{
    public ReferenceMapping()
    {
        CreateMap<Reference, ReferenceDto>()
            .IncludeAllDerived();
    }
}