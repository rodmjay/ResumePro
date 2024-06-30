using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<Resume, ResumeDto>()
            .IncludeAllDerived();

        CreateMap<Resume, ResumeDetails>()
            .ForMember(x => x.Jobs, opt => opt.MapFrom(x => x.Jobs))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills));
    }
}