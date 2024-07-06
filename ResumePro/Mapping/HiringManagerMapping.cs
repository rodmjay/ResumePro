using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class HiringManagerMapping : Profile
{
    public HiringManagerMapping()
    {
        CreateMap<HiringManager, HiringManagerDto>()
            .IncludeAllDerived();
    }
}